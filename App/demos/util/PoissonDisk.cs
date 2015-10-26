using OpenTK;
using System;
using System.Collections.Generic;

namespace util
{
    using System.Globalization;
    using Commands = Dictionary<string, string[]>;

    class PoissonDisk
    {
        #region FIELDS
        private string name = "PoissonDisk";
        public int maxSamples = 0;
        public int numRadii = 0;
        public float minRadius = 0f;
        private Vector2[] samples;
        private int[] radius;
        protected List<string> errors = new List<string>();
        #endregion

        #region PROPERTIES
        public string Name { get { return name; } set { name = value; } }
        public int MaxSamples { get { return maxSamples; } set { maxSamples = value; } }
        public float MinRadius { get { return minRadius; } set { minRadius = value; } }
        public int NumRadii { get { return numRadii; } set { numRadii = value; } }
        #endregion

        public PoissonDisk(Commands cmds)
        {
            // PARSE COMMAND VALUES SPECIFIED BY THE USER
            Convert(cmds, "name", ref name);
            Convert(cmds, "maxSamples", ref maxSamples);
            Convert(cmds, "minRadius", ref minRadius);
            Convert(cmds, "numRadii", ref numRadii);

            if (minRadius <= 0f)
            {
                errors.Add("At least 'radius' (minimal sample distance) "
                    + "needs to be defined and has to be bigger than 0.0.");
                return;
            }

            // CREATE POISSON DISK

            //var points = PoissonGen.Disk(minRadius);
            var points = new PoissonDiscSampler(minRadius).Samples();

            // SORT POISSON DISK POINTS BY DESCENDING DISTANCE TO EACH OTHER

            maxSamples = maxSamples > 0 ? Math.Min(points.Count, maxSamples) : points.Count;
            List<Vector2> sortedPoints = new List<Vector2>(maxSamples);
            List<float> sortedDist = new List<float>(maxSamples);

            // find center point
            int idx = ClosestPoint(points, new Vector2(0f, 0f));
            sortedPoints.Add(points[idx]);
            sortedDist.Add(float.MaxValue);
            points.RemoveAt(idx);

            // sort points
            for (int i = 1; i < maxSamples; i++)
            {
                float r;
                // find most distant non-sorted point in point list
                idx = MostDistantPoint(points, sortedPoints, out r);
                // add point to sorted list and remove it from non-sorted list
                sortedPoints.Add(points[idx]);
                sortedDist.Add(r);
                points.RemoveAt(idx);
            }

            // save the result
            samples = sortedPoints.ToArray();

            // CREATE RADIUS LOOKUP TABLE

            if (numRadii <= 0)
                return;
            var dist = sortedDist.ToArray();
            radius = new int[Math.Max(numRadii, 1)];

            for (int i = 0, j = 0; i < numRadii; i++)
            {
                // interpolate radius value
                var t = i / (float)numRadii;
                var r = (1f - t);
                // find first distance smaller than the interpolated radius
                for (; j < maxSamples; j++)
                    if (dist[j] <= r)
                        break;
                // save index in lookup table
                radius[i] = j;
            }
        }

        public void Update(int program, int width, int height, int widthTex, int heightTex)
        {

        }

        public List<string> GetErrors()
        {
            return errors;
        }

        #region UTILITY METHOD
        private static int ClosestPoint(List<Vector2> points, Vector2 query)
        {
            int i = 0;
            int idx = 0;
            float minDist = float.MaxValue;

            foreach (var point in points)
            {
                var dist = (point - query).LengthSquared;
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
            int i = 0;
            int idx = 0;
            maxDist = 0f;

            foreach (var point in points)
            {
                var minDistToQueries = float.MaxValue;
                foreach (var query in queries)
                    minDistToQueries = Math.Min((point - query).LengthSquared, minDistToQueries);

                if (minDistToQueries > maxDist)
                {
                    maxDist = minDistToQueries;
                    idx = i;
                }

                i++;
            }

            return idx;
        }

        private void Convert<T>(Commands cmds, string cmd, ref T v)
        {
            if (cmds.ContainsKey(cmd))
            {
                var s = cmds[cmd];
                if (s.Length == 0)
                    return;
                if (!TryChangeType(s[0], ref v))
                    errors.Add("Command '" + cmd + "': Could not convert argument 1 '" + s[0] + "'.");
            }
        }

        private static CultureInfo culture = new CultureInfo("en");

        private static bool TryChangeType<T>(object invalue, ref T value)
        {

            if (invalue == null || invalue as IConvertible == null)
                return false;

            try
            {
                value = (T)System.Convert.ChangeType(invalue, typeof(T), culture);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }

    /// Poisson-disc sampling using Bridson's algorithm.
    /// Adapted from Mike Bostock's Javascript source: http://bl.ocks.org/mbostock/19168c663618b7f07158
    ///
    /// See here for more information about this algorithm:
    ///   http://devmag.org.za/2009/05/03/poisson-disk-sampling/
    ///   http://bl.ocks.org/mbostock/dbb02448b0f93e4c82c3
    ///
    /// Usage:
    ///   PoissonDiscSampler sampler = new PoissonDiscSampler(10, 5, 0.3f);
    ///   foreach (Vector2 sample in sampler.Samples()) {
    ///       // ... do something, like instantiate an object at (sample.x, sample.y) for example:
    ///       Instantiate(someObject, new Vector3(sample.x, 0, sample.y), Quaternion.identity);
    ///   }
    ///
    /// Author: Gregory Schlomoff (gregory.schlomoff@gmail.com)
    /// Released in the public domain
    public class PoissonDiscSampler
    {
        private const int k = 30;  // Maximum number of attempts before marking a sample as inactive.

        private static Random rand = new Random();
        private readonly float radius2;  // radius squared
        private readonly float cellSize;
        private Vector2[,] grid;
        private List<Vector2> activeSamples = new List<Vector2>();

        /// Create a sampler with the following parameters:
        ///
        /// width:  each sample's x coordinate will be between [0, width]
        /// height: each sample's y coordinate will be between [0, height]
        /// radius: each sample will be at least `radius` units away from any other sample, and at most 2 * `radius`.
        public PoissonDiscSampler(float radius)
        {
            radius2 = radius * radius;
            cellSize = radius / (float)Math.Sqrt(2);
            grid = new Vector2[(int)(1 / cellSize), (int)(1 / cellSize)];
        }

        /// Return a lazy sequence of samples. You typically want to call this in a foreach loop, like so:
        ///   foreach (Vector2 sample in sampler.Samples()) { ... }
        public List<Vector2> Samples()
        {
            List<Vector2> points = new List<Vector2>();

            // First sample is choosen randomly
            AddSample(new Vector2(0f, 0f));

            while (activeSamples.Count > 0)
            {
                // Pick a random active sample
                int i = (int)rand.NextDouble() * activeSamples.Count;
                Vector2 sample = activeSamples[i];

                // Try `k` random candidates between [radius, 2 * radius] from that sample.
                bool found = false;
                for (int j = 0; j < k; ++j)
                {

                    var angle = 2 * Math.PI * rand.NextDouble();
                    // See: http://stackoverflow.com/questions/9048095/create-random-number-within-an-annulus/9048443#9048443
                    var r = (float)Math.Sqrt(rand.NextDouble() * 3 * radius2 + radius2);
                    Vector2 candidate = sample + r * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

                    // Accept candidates if it's inside the rect and farther than 2 * radius to any existing sample.
                    if (candidate.Length <= 1f && IsFarEnough(candidate))
                    {
                        found = true;
                        points.Add(AddSample(candidate));
                        break;
                    }
                }

                // If we couldn't find a valid candidate after k attempts, remove this sample from the active samples queue
                if (!found)
                {
                    activeSamples[i] = activeSamples[activeSamples.Count - 1];
                    activeSamples.RemoveAt(activeSamples.Count - 1);
                }
            }

            return points;
        }

        private bool IsFarEnough(Vector2 sample)
        {
            Vector2 shift = new Vector2(0.5f, 0.5f);
            GridPos pos = new GridPos(sample * 0.5f + shift, cellSize);

            pos.x = Math.Min(pos.x, grid.GetLength(0) - 1);
            pos.y = Math.Min(pos.y, grid.GetLength(1) - 1);
            if (grid[pos.x, pos.y] != Vector2.Zero)
                return false;

            int xmin = Math.Max(pos.x - 2, 0);
            int ymin = Math.Max(pos.y - 2, 0);
            int xmax = Math.Min(pos.x + 2, grid.GetLength(0) - 1);
            int ymax = Math.Min(pos.y + 2, grid.GetLength(1) - 1);

            for (int y = ymin; y <= ymax; y++)
            {
                for (int x = xmin; x <= xmax; x++)
                {
                    Vector2 s = grid[x, y];
                    if (s != Vector2.Zero)
                    {
                        Vector2 d = s - sample;
                        if (d.X * d.X + d.Y * d.Y < radius2)
                            return false;
                    }
                }
            }

            return true;

            // Note: we use the zero vector to denote an unfilled cell in the grid. This means that if we were
            // to randomly pick (0, 0) as a sample, it would be ignored for the purposes of proximity-testing
            // and we might end up with another sample too close from (0, 0). This is a very minor issue.
        }

        /// Adds the sample to the active samples queue and the grid before returning it
        private Vector2 AddSample(Vector2 sample)
        {
            activeSamples.Add(sample);
            Vector2 shift = new Vector2(0.5f, 0.5f);
            GridPos pos = new GridPos(sample * 0.5f + shift, cellSize);
            pos.x = Math.Min(pos.x, grid.GetLength(0) - 1);
            pos.y = Math.Min(pos.y, grid.GetLength(1) - 1);
            grid[pos.x, pos.y] = sample;
            return sample;
        }

        /// Helper struct to calculate the x and y indices of a sample in the grid
        private struct GridPos
        {
            public int x;
            public int y;

            public GridPos(Vector2 sample, float cellSize)
            {
                x = (int)(sample.X / cellSize);
                y = (int)(sample.Y / cellSize);
            }
        }
    }
}