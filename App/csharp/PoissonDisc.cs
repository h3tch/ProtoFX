using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using Commands = System.Collections.Generic.Dictionary<string, string[]>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;

namespace csharp
{
    class PoissonDisc : CsObject
    {
        public enum Names
        {
            numPoints,
            radius,
            points,
        }

        #region FIELDS
        private string name = "PoissonDisc";
        public int maxPoints = 0;
        public int numRadii = 0;
        public float minRadius = 0f;
        private float[,] points;
        private int[] radius;
        protected Dictionary<int, UniformBlock<Names>> uniform =
            new Dictionary<int, UniformBlock<Names>>();
        #endregion

        // Properties accessible by ProtoGL
        #region PROPERTIES
        public string Name { get { return name; } }
        public int MaxPoints { get { return maxPoints; } }
        public float MinRadius { get { return minRadius; } }
        public int NumRadii { get { return numRadii; } }
        #endregion

        public PoissonDisc(string name, Commands cmds, GLNames glNames)
        {
            // PARSE COMMAND VALUES SPECIFIED BY THE USER
            this.name = name;
            Convert(cmds, "name", ref this.name);
            Convert(cmds, "maxPoints", ref maxPoints);
            Convert(cmds, "minRadius", ref minRadius);
            Convert(cmds, "numRadii", ref numRadii);

            if (minRadius <= 0f)
            {
                errors.Add("At least 'minRadius' (minimal sample distance) "
                    + "needs to be defined and has to be bigger than 0.0.");
                return;
            }

            // CREATE POISSON DISK
            
            var points = new Disc(minRadius).Points;

            // SORT POISSON DISK POINTS BY DESCENDING DISTANCE TO EACH OTHER

            maxPoints = maxPoints > 0 ? Math.Min(points.Count, maxPoints) : points.Count;
            List<Vector2> sortedPoints = new List<Vector2>(maxPoints);
            List<float> sortedDist = new List<float>(maxPoints);

            // find center point
            int idx = ClosestPoint(points, new Vector2(0f, 0f));
            sortedPoints.Add(points[idx]);
            sortedDist.Add(float.MaxValue);
            points[idx] = points[points.Count - 1];
            points.RemoveAt(points.Count - 1);

            // sort points
            for (int i = 1; i < maxPoints; i++)
            {
                float r;
                // find most distant non-sorted point in point list
                idx = MostDistantPoint(points, sortedPoints, out r);
                // add point to sorted list and remove it from non-sorted list
                sortedPoints.Add(points[idx]);
                sortedDist.Add(r);
                points[idx] = points[points.Count - 1];
                points.RemoveAt(points.Count - 1);
            }

            // save the result
            this.points = new float[sortedPoints.Count, 2];
            int iter = 0;
            foreach (var point in sortedPoints)
            {
                this.points[iter, 0] = point.X;
                this.points[iter, 1] = point.Y;
                iter++;
            }

            // CREATE RADIUS LOOKUP TABLE

            if (numRadii <= 0)
                return;

            var dist = sortedDist.ToArray();
            radius = new int[Math.Max(numRadii, 1)];

            for (int i = 0, j = 0; i < numRadii; i++)
            {
                // interpolate radius value
                var t = i / (float)numRadii;
                var r = 1f - t;
                // find first distance smaller than the interpolated radius
                for (; j < maxPoints; j++)
                    if (dist[j] <= r)
                        break;
                // save index in lookup table
                radius[i] = j;
            }
        }

        public void Update(int program, int width, int height, int widthTex, int heightTex)
        {
            // GET OR CREATE POISSON DISC UNIFORMS FOR program
            UniformBlock<Names> unif;
            if (uniform.TryGetValue(program, out unif) == false)
            {
                uniform.Add(program, unif = new UniformBlock<Names>(program, name));
                // SET UNIFORM VALUES
                unif.Set(Names.numPoints, new[] { points.GetLength(0) });
                unif.Set(Names.radius, radius);
                unif.Set(Names.points, points);
                // UPDATE UNIFORM BUFFER
                unif.Update();
            }

            unif.Bind();
        }

        public void Delete()
        {
            foreach (var u in uniform)
                u.Value.Delete();
        }
        
        #region UTILITY METHOD
        private static int ClosestPoint(List<Vector2> points, Vector2 query)
        {
            int i = 0, idx = 0;
            float minDist = float.MaxValue;

            // find minimal distance and index to query point
            foreach (var point in points)
            {
                var dist = (point - query).LengthSquared;

                // cache minimal distance
                if (dist < minDist)
                {
                    minDist = dist;
                    idx = i;
                }

                i++;
            }

            return idx;
        }

        private static int MostDistantPoint(List<Vector2> points, List<Vector2> queries, out float maxDist)
        {
            int i = 0, idx = 0;
            maxDist = 0f;

            // find maximal distance and index to all query points
            foreach (var point in points)
            {
                // get closest query point distance
                var minDistToQueries = float.MaxValue;
                foreach (var query in queries)
                    minDistToQueries = Math.Min((point - query).LengthSquared, minDistToQueries);

                // cache maximal distance
                if (minDistToQueries > maxDist)
                {
                    maxDist = minDistToQueries;
                    idx = i;
                }

                i++;
            }

            return idx;
        }
        #endregion

        #region INNER CLASSES
        class Disc
        {
            private static Random random = new Random();
            public List<Vector2> Points
            { get; private set; }

            public Disc(float r, int resolution = 8)
            {
                var indices = new List<int>();

                // create grid
                var gridsize = (((int)(1 / (r / resolution)) + 1) / 2) * 2;
                //var grid = Enumerable.Range(0, gridsize * gridsize).ToList();
                var grid = ComputeGridIdx(gridsize).ToList();
                var mask = ComputeMask(resolution);

                try
                {
                    // Poisson sampling of the grid
                    while (grid.Count > 0)
                    {
                        // pick random grid point
                        int idx = grid[random.Next(grid.Count)];
                        indices.Add(idx);
                        int Cy = idx / gridsize, Cx = idx - Cy * gridsize;

                        // remove all grid points within the mask
                        for (int cur = 0, i = 0; i < mask.Length; i++)
                        {
                            int Px = Cx + mask[i][0];
                            if (Px < 0 || Px >= gridsize)
                                continue;
                            int Py = Cy + mask[i][1];
                            if (Py < 0 || Py >= gridsize)
                                continue;
                            int rem = gridsize * Py + Px;
                            while (cur < grid.Count && grid[cur] < rem)
                                cur++;
                            if (cur < grid.Count && grid[cur] == rem)
                                grid.RemoveAt(cur);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }

                // convert indices to UV coordinates
                Points = Indices2Uv(indices, gridsize).ToList();
            }

            private IEnumerable<int> ComputeGridIdx(int gridsize)
            {
                int r = gridsize / 2;
                int min = -r;
                int max = r;
                for (int y = min, idx = 0; y < max; y++)
                {
                    for (int x = min; x < max; x++, idx++)
                    {
                        if (x * x + y * y < r * r)
                            yield return idx;
                    }
                }
            }

            private int[][] ComputeMask(int resolution)
            {
                var mask = new List<int[]>();

                for (int y = -resolution; y <= resolution; y++)
                {
                    for (int x = -resolution; x <= resolution; x++)
                        if (x * x + y * y <= resolution * resolution)
                            mask.Add(new[] { x, y });
                }

                return mask.ToArray();
            }

            private IEnumerable<Vector2> Indices2Uv(IEnumerable<int> indices, int gridsize)
            {
                foreach (var i in indices)
                {
                    int Y = i / gridsize;
                    float y = (float)Y / gridsize;
                    float x = (float)(i - (Y * gridsize)) / gridsize;
                    yield return new Vector2(x * 2 - 1, y * 2 - 1);
                }
            }
        }
        #endregion
    }
}