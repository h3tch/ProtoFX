using System;
using System.Collections.Generic;

namespace util
{
    using OpenTK;
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

            var center = new Vector2(0f, 0f);
            var points = PoissonDiskSampler.SampleCircle(center, 1f, minRadius);

            // SORT POISSON DISK POINTS BY DESCENDING DISTANCE TO EACH OTHER

            maxSamples = maxSamples > 0 ? Math.Min(points.Count, maxSamples) : points.Count;
            List<Vector2> sortedPoints = new List<Vector2>(maxSamples);
            List<float> sortedDist = new List<float>(maxSamples);

            // find center point
            int idx = ClosestPoint(points, center);
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
                var t = (i + 1) / (float)numRadii;
                var r = minRadius * (t - 1f) + minRadius * t;
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
            return 0;
        }

        private static int MostDistantPoint(List<Vector2> points, List<Vector2> query, out float r)
        {
            r = 0;
            return 0;
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

        private static bool TryChangeType<T>(object invalue, ref T value)
        {
            if (invalue == null || invalue as IConvertible == null)
                return false;

            try
            {
                value = (T)System.Convert.ChangeType(invalue, typeof(T));
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }

    // Based on http://theinstructionlimit.com/fast-uniform-poisson-disk-sampling-in-c

    // Adapated from java source by Herman Tulleken
    // http://www.luma.co.za/labs/2008/02/27/poisson-disk-sampling/

    // The algorithm is from the "Fast Poisson Disk Sampling in Arbitrary Dimensions" paper by Robert Bridson
    // http://www.cs.ubc.ca/~rbridson/docs/bridson-siggraph07-poissondisk.pdf

    public static class PoissonDiskSampler
    {
        public const int DefaultPointsPerIteration = 30;

        static readonly float SquareRootTwo = (float)Math.Sqrt(2);

        struct Settings
        {
            public Vector2 TopLeft, LowerRight, Center;
            public Vector2 Dimensions;
            public float? RejectionSqDistance;
            public float MinimumDistance;
            public float CellSize;
            public int GridWidth, GridHeight;
        }

        struct State
        {
            public Vector2?[,] Grid;
            public List<Vector2> ActivePoints, Points;
        }

        public static List<Vector2> SampleCircle(Vector2 center, float radius, float minimumDistance)
        {
            return SampleCircle(center, radius, minimumDistance, DefaultPointsPerIteration);
        }
        public static List<Vector2> SampleCircle(Vector2 center, float radius, float minimumDistance, int pointsPerIteration)
        {
            return Sample(center - new Vector2(radius), center + new Vector2(radius), radius, minimumDistance, pointsPerIteration);
        }

        public static List<Vector2> SampleRectangle(Vector2 topLeft, Vector2 lowerRight, float minimumDistance)
        {
            return SampleRectangle(topLeft, lowerRight, minimumDistance, DefaultPointsPerIteration);
        }
        public static List<Vector2> SampleRectangle(Vector2 topLeft, Vector2 lowerRight, float minimumDistance, int pointsPerIteration)
        {
            return Sample(topLeft, lowerRight, null, minimumDistance, pointsPerIteration);
        }

        static List<Vector2> Sample(Vector2 topLeft, Vector2 lowerRight, float? rejectionDistance, float minimumDistance, int pointsPerIteration)
        {
            var settings = new Settings
            {
                TopLeft = topLeft,
                LowerRight = lowerRight,
                Dimensions = lowerRight - topLeft,
                Center = (topLeft + lowerRight) / 2,
                CellSize = minimumDistance / SquareRootTwo,
                MinimumDistance = minimumDistance,
                RejectionSqDistance = rejectionDistance == null ? null : rejectionDistance * rejectionDistance
            };
            settings.GridWidth = (int)(settings.Dimensions.X / settings.CellSize) + 1;
            settings.GridHeight = (int)(settings.Dimensions.Y / settings.CellSize) + 1;

            var state = new State
            {
                Grid = new Vector2?[settings.GridWidth, settings.GridHeight],
                ActivePoints = new List<Vector2>(),
                Points = new List<Vector2>()
            };

            AddFirstPoint(ref settings, ref state);

            while (state.ActivePoints.Count != 0)
            {
                var listIndex = RandomHelper.Random.Next(state.ActivePoints.Count);

                var point = state.ActivePoints[listIndex];
                var found = false;

                for (var k = 0; k < pointsPerIteration; k++)
                    found |= AddNextPoint(point, ref settings, ref state);

                if (!found)
                    state.ActivePoints.RemoveAt(listIndex);
            }

            return state.Points;
        }

        static void AddFirstPoint(ref Settings settings, ref State state)
        {
            var added = false;
            while (!added)
            {
                var d = RandomHelper.Random.NextDouble();
                var xr = settings.TopLeft.X + settings.Dimensions.X * d;

                d = RandomHelper.Random.NextDouble();
                var yr = settings.TopLeft.Y + settings.Dimensions.Y * d;

                var p = new Vector2((float)xr, (float)yr);
                if (settings.RejectionSqDistance != null && Vector2.Dot(settings.Center, p) > settings.RejectionSqDistance)
                    continue;
                added = true;

                var index = Denormalize(p, settings.TopLeft, settings.CellSize);

                state.Grid[(int)index.X, (int)index.Y] = p;

                state.ActivePoints.Add(p);
                state.Points.Add(p);
            }
        }

        static bool AddNextPoint(Vector2 point, ref Settings settings, ref State state)
        {
            var found = false;
            var q = GenerateRandomAround(point, settings.MinimumDistance);

            if (q.X >= settings.TopLeft.X && q.X < settings.LowerRight.X &&
                q.Y > settings.TopLeft.Y && q.Y < settings.LowerRight.Y &&
                (settings.RejectionSqDistance == null || Vector2.Dot(settings.Center, q) <= settings.RejectionSqDistance))
            {
                var qIndex = Denormalize(q, settings.TopLeft, settings.CellSize);
                var tooClose = false;

                for (var i = (int)Math.Max(0, qIndex.X - 2); i < Math.Min(settings.GridWidth, qIndex.X + 3) && !tooClose; i++)
                    for (var j = (int)Math.Max(0, qIndex.Y - 2); j < Math.Min(settings.GridHeight, qIndex.Y + 3) && !tooClose; j++)
                        if (state.Grid[i, j].HasValue && Math.Sqrt(Vector2.Dot(state.Grid[i, j].Value, q)) < settings.MinimumDistance)
                            tooClose = true;

                if (!tooClose)
                {
                    found = true;
                    state.ActivePoints.Add(q);
                    state.Points.Add(q);
                    state.Grid[(int)qIndex.X, (int)qIndex.Y] = q;
                }
            }
            return found;
        }

        static Vector2 GenerateRandomAround(Vector2 center, float minimumDistance)
        {
            var d = RandomHelper.Random.NextDouble();
            var radius = minimumDistance + minimumDistance * d;

            d = RandomHelper.Random.NextDouble();
            var angle = MathHelper.TwoPi * d;

            var newX = radius * Math.Sin(angle);
            var newY = radius * Math.Cos(angle);

            return new Vector2((float)(center.X + newX), (float)(center.Y + newY));
        }

        static Vector2 Denormalize(Vector2 point, Vector2 origin, double cellSize)
        {
            return new Vector2((int)((point.X - origin.X) / cellSize), (int)((point.Y - origin.Y) / cellSize));
        }
    }

    public static class RandomHelper
    {
        public static readonly Random Random = new Random();
    }

    public static class MathHelper
    {
        public const float Pi = (float)Math.PI;
        public const float HalfPi = (float)(Math.PI / 2);
        public const float TwoPi = (float)(Math.PI * 2);
    }
}
