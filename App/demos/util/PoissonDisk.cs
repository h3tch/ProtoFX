using OpenTK;
using System;
using System.Collections.Generic;

namespace util
{
    using System.Globalization;
    using System.Threading;
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
            
            var points = PoissonGen.Disk(minRadius);

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

    public static class PoissonGen
    {
        static Random rand = new Random();
        private const float TwoPI = (float)(2 * Math.PI);

        public static List<Vector2> Disk(float radius)
        {
            List<Circle> circles = new List<Circle>();
            List<Circle> active = new List<Circle>();

            // compute minimal step size
            float min, max;
            if (Intersect(new Vector2(0f, 0f), 1f, new Vector2(1f, 0f), radius, out min, out max) == 0)
                return null;
            float step = max - min;

            Intersect(new Vector2(0f, 0f), radius, new Vector2(radius, 0f), radius, out min, out max);
            float halfArcLec = (max - min) * 0.5f;

            // REPEAT RANDOMLY ADDING POINTS UNTIL NO MORE POINTS CAN BE ADDED

            var circle = new Circle(new Vector2(0f, 0f), radius);
            circles.Add(circle);
            active.Add(circle);
            while (active.Count > 0)
            {
                circle = active[0];
                circle.NewCircles(halfArcLec, circles, active);
                active.Remove(circle);
                Thread.Sleep(1);
            }

            // CONVERT TO VECTOR ARRAY

            List<Vector2> disk = new List<Vector2>(circles.Count);

            foreach (var c in circles)
                disk.Add(c.pos);

            return disk;
        }

        private static int Intersect(Vector2 A, float Ar, Vector2 B, float Br,
            out float from, out float to)
        {
            // Find the distance between the centers.
            Vector2 delta = A - B;
            float distsq = delta.LengthSquared;
            float dist = (float)Math.Sqrt(distsq);

            if (// No solutions, the circles are too far apart.
                (dist > Ar + Br) ||
                // No solutions, one circle contains the other.
                (dist < Math.Abs(Ar - Br)) ||
                // No solutions, the circles coincide.
                ((dist == 0) && (Ar == Br)))
            {
                from = float.NaN;
                to = float.NaN;
                return 0;
            }
            else
            {
                // Find a and h.
                float a = (Ar * Ar - Br * Br + distsq) / (2 * dist);
                float h = (float)Math.Sqrt(Ar * Ar - a * a);

                // Find P2.
                delta = B - A;
                Vector2 mid = A + a * delta / dist;

                // Get the points P3.
                delta.Y *= -1;
                Vector2 v = (h * delta / dist).Yx;
                Vector2 left = mid + v;
                Vector2 right = mid - v;

                // compute min
                v = Vector2.NormalizeFast(right - A);
                from = (float)Math.Acos(v.X);
                if (v.Y < 0f)
                    from = TwoPI - from;

                // compute max
                v = Vector2.NormalizeFast(left - A);
                to = (float)Math.Acos(v.X);
                if (v.Y < 0f)
                    to = TwoPI - to;

                // See if we have 1 or 2 solutions.
                if (to < from)
                    to += TwoPI;
                return (dist == Ar + Br) ? 1 : 2;
            }
        }

        public class Circle
        {
            public Vector2 pos;
            private Circle prev, next, parent;
            private float min, max;
            private static float radius;
            public Circle Prev { get { return prev; } set { prev = value; UpdateMinMax(value); } }
            public Circle Next { get { return next; } set { next = value; UpdateMinMax(value); } }
            public Circle Parent { get { return parent; } set { parent = value; UpdateMinMax(value); } }

            public Circle(Vector2 pos, float radius, Circle parent = null)
            {
                Circle.radius = radius;
                this.pos = pos;
                this.prev = null;
                this.next = null;
                this.min = 0f;
                this.max = 2 * TwoPI;
                if (parent != null)
                    this.Parent = parent;
                else
                    this.max = TwoPI;
            }

            public void NewCircles(float halfArc, List<Circle> circles, List<Circle> active)
            {
                Circle prev = this, newCircle = null;
                int first = circles.Count;

                while (min < max)
                {
                    var t = min + (max - min) * (float)rand.NextDouble();
                    while (t - halfArc > min && t + halfArc < max)
                        t -= halfArc;
                    min = t + halfArc;

                    Vector2 p = pos + new Vector2(
                        radius * (float)Math.Cos(t),
                        radius * (float)Math.Sin(t));

                    if (p.LengthSquared > (1 + radius) * (1 + radius))
                        return;

                    newCircle = new Circle(p, radius, this);
                    newCircle.Prev = prev;
                    prev.Next = newCircle;
                    prev = newCircle;

                    circles.Add(newCircle);
                    active.Add(newCircle);
                }

                if (newCircle != null)
                    newCircle.Next = circles[first];
            }

            private void UpdateMinMax(Circle circle)
            {
                if (circle == null)
                    return;
                float from, to;
                Intersect(pos, radius, circle.pos, radius, out from, out to);
                max = Math.Min(from, max);
                min = Math.Max(to, min);
                if (min > TwoPI)
                    min -= TwoPI;
                if (max < min)
                    max += TwoPI;
            }
        }
    }

    //// Based on http://theinstructionlimit.com/fast-uniform-poisson-disk-sampling-in-c

    //// Adapated from java source by Herman Tulleken
    //// http://www.luma.co.za/labs/2008/02/27/poisson-disk-sampling/

    //// The algorithm is from the "Fast Poisson Disk Sampling in Arbitrary Dimensions" paper by Robert Bridson
    //// http://www.cs.ubc.ca/~rbridson/docs/bridson-siggraph07-poissondisk.pdf

    //public static class PoissonDiskSampler
    //{
    //    public const int DefaultPointsPerIteration = 30;

    //    static readonly float SquareRootTwo = (float)Math.Sqrt(2);

    //    struct Settings
    //    {
    //        public Vector2 TopLeft, LowerRight, Center;
    //        public Vector2 Dimensions;
    //        public float? RejectionSqDistance;
    //        public float MinimumDistance;
    //        public float CellSize;
    //        public int GridWidth, GridHeight;
    //    }

    //    struct State
    //    {
    //        public Vector2?[,] Grid;
    //        public List<Vector2> ActivePoints, Points;
    //    }

    //    public static List<Vector2> SampleCircle(Vector2 center, float radius, float minimumDistance)
    //    {
    //        return SampleCircle(center, radius, minimumDistance, DefaultPointsPerIteration);
    //    }
    //    public static List<Vector2> SampleCircle(Vector2 center, float radius, float minimumDistance, int pointsPerIteration)
    //    {
    //        return Sample(center - new Vector2(radius), center + new Vector2(radius), radius, minimumDistance, pointsPerIteration);
    //    }

    //    public static List<Vector2> SampleRectangle(Vector2 topLeft, Vector2 lowerRight, float minimumDistance)
    //    {
    //        return SampleRectangle(topLeft, lowerRight, minimumDistance, DefaultPointsPerIteration);
    //    }
    //    public static List<Vector2> SampleRectangle(Vector2 topLeft, Vector2 lowerRight, float minimumDistance, int pointsPerIteration)
    //    {
    //        return Sample(topLeft, lowerRight, null, minimumDistance, pointsPerIteration);
    //    }

    //    static List<Vector2> Sample(Vector2 topLeft, Vector2 lowerRight, float? rejectionDistance, float minimumDistance, int pointsPerIteration)
    //    {
    //        var settings = new Settings
    //        {
    //            TopLeft = topLeft,
    //            LowerRight = lowerRight,
    //            Dimensions = lowerRight - topLeft,
    //            Center = (topLeft + lowerRight) / 2,
    //            CellSize = minimumDistance / SquareRootTwo,
    //            MinimumDistance = minimumDistance,
    //            RejectionSqDistance = rejectionDistance == null ? null : rejectionDistance * rejectionDistance
    //        };
    //        settings.GridWidth = (int)(settings.Dimensions.X / settings.CellSize) + 1;
    //        settings.GridHeight = (int)(settings.Dimensions.Y / settings.CellSize) + 1;

    //        var state = new State
    //        {
    //            Grid = new Vector2?[settings.GridWidth, settings.GridHeight],
    //            ActivePoints = new List<Vector2>(),
    //            Points = new List<Vector2>()
    //        };

    //        AddFirstPoint(ref settings, ref state);

    //        while (state.ActivePoints.Count != 0)
    //        {
    //            var listIndex = RandomHelper.Random.Next(state.ActivePoints.Count);

    //            var point = state.ActivePoints[listIndex];
    //            var found = false;

    //            for (var k = 0; k < pointsPerIteration; k++)
    //                found |= AddNextPoint(point, ref settings, ref state);

    //            if (!found)
    //                state.ActivePoints.RemoveAt(listIndex);
    //        }

    //        return state.Points;
    //    }

    //    static void AddFirstPoint(ref Settings settings, ref State state)
    //    {
    //        var added = false;
    //        while (!added)
    //        {
    //            var d = RandomHelper.Random.NextDouble();
    //            var xr = settings.TopLeft.X + settings.Dimensions.X * d;

    //            d = RandomHelper.Random.NextDouble();
    //            var yr = settings.TopLeft.Y + settings.Dimensions.Y * d;

    //            var p = new Vector2((float)xr, (float)yr);
    //            if (settings.RejectionSqDistance != null && Vector2.Dot(settings.Center, p) > settings.RejectionSqDistance)
    //                continue;
    //            added = true;

    //            var index = Denormalize(p, settings.TopLeft, settings.CellSize);

    //            state.Grid[(int)index.X, (int)index.Y] = p;

    //            state.ActivePoints.Add(p);
    //            state.Points.Add(p);
    //        }
    //    }

    //    static bool AddNextPoint(Vector2 point, ref Settings settings, ref State state)
    //    {
    //        var found = false;
    //        var q = GenerateRandomAround(point, settings.MinimumDistance);

    //        if (q.X >= settings.TopLeft.X && q.X < settings.LowerRight.X &&
    //            q.Y > settings.TopLeft.Y && q.Y < settings.LowerRight.Y &&
    //            (settings.RejectionSqDistance == null || Vector2.Dot(settings.Center, q) <= settings.RejectionSqDistance))
    //        {
    //            var qIndex = Denormalize(q, settings.TopLeft, settings.CellSize);
    //            var tooClose = false;

    //            for (var i = (int)Math.Max(0, qIndex.X - 2); i < Math.Min(settings.GridWidth, qIndex.X + 3) && !tooClose; i++)
    //                for (var j = (int)Math.Max(0, qIndex.Y - 2); j < Math.Min(settings.GridHeight, qIndex.Y + 3) && !tooClose; j++)
    //                    if (state.Grid[i, j].HasValue && Math.Sqrt(Vector2.Dot(state.Grid[i, j].Value, q)) < settings.MinimumDistance)
    //                        tooClose = true;

    //            if (!tooClose)
    //            {
    //                found = true;
    //                state.ActivePoints.Add(q);
    //                state.Points.Add(q);
    //                state.Grid[(int)qIndex.X, (int)qIndex.Y] = q;
    //            }
    //        }
    //        return found;
    //    }

    //    static Vector2 GenerateRandomAround(Vector2 center, float minimumDistance)
    //    {
    //        var d = RandomHelper.Random.NextDouble();
    //        var radius = minimumDistance + minimumDistance * d;

    //        d = RandomHelper.Random.NextDouble();
    //        var angle = MathHelper.TwoPi * d;

    //        var newX = radius * Math.Sin(angle);
    //        var newY = radius * Math.Cos(angle);

    //        return new Vector2((float)(center.X + newX), (float)(center.Y + newY));
    //    }

    //    static Vector2 Denormalize(Vector2 point, Vector2 origin, double cellSize)
    //    {
    //        return new Vector2((int)((point.X - origin.X) / cellSize), (int)((point.Y - origin.Y) / cellSize));
    //    }
    //}

    //public static class RandomHelper
    //{
    //    public static readonly Random Random = new Random();
    //}

    //public static class MathHelper
    //{
    //    public const float Pi = (float)Math.PI;
    //    public const float HalfPi = (float)(Math.PI / 2);
    //    public const float TwoPi = (float)(Math.PI * 2);
    //}
}
