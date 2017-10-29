using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;

namespace sampling
{
    class PoissonDisc : protofx.Object
    {
        #region UNIFORM NAMES

        public enum Names
        {
            numPoints,
            points,
            distances,
            indices,
            LAST,
        }
        protected static string[] UniformNames = Enum.GetNames(typeof(Names))
            .Take((int)Names.LAST).ToArray();

        #endregion

        #region FIELDS

        private string name = "PoissonDisc";
        public int maxPoints = 0;
        public float minRadius = 0f;
        public float[,] points;
        public float[,] distances;
        public int[,] indices;

        #endregion

        // Properties accessible by ProtoGL
        #region PROPERTIES

        public string Name { get { return name; } }
        public int MaxPoints { get { return maxPoints; } }
        public float MinRadius { get { return minRadius; } }
        public int NumPoints { get; private set; }

        #endregion

        public PoissonDisc(string name, Commands cmds, Objects objs, GLNames glNames)
            : base(cmds, objs)
        {
            // PARSE COMMAND VALUES SPECIFIED BY THE USER

            this.name = name;
            Convert(cmds, "name", ref this.name);
            Convert(cmds, "maxPoints", ref maxPoints);
            Convert(cmds, "nPoints", ref maxPoints);
            Convert(cmds, "minRadius", ref minRadius);
            Convert(cmds, "minDist", ref minRadius);

            // CREATE POISSON DISK
            if (minRadius <= 0.0f && maxPoints <= 0)
                maxPoints = 1000;
            if (minRadius <= 0.0f)
                minRadius = (float)Math.Sqrt((float)maxPoints) / maxPoints;
            if (maxPoints <= 0)
                maxPoints = (int)(1 / (minRadius * minRadius));

            var pointList = PoissonSampler.SampleCircle(new Vector2(0f, 0f), 1f, minRadius);
            NumPoints = pointList.Count;
            points = new float[NumPoints, 2];
            distances = new float[NumPoints, NumPoints];
            indices = new int[NumPoints, NumPoints];

            for (int i = 0; i < NumPoints; i++)
            {
                points[i, 0] = pointList[i].X;
                points[i, 1] = pointList[i].Y;
            }

            var dist = new float[NumPoints];
            var ind = Enumerable.Range(0, NumPoints).ToArray();
            for (int i = 0; i < NumPoints; i++)
            {
                for (int j = 0; j < NumPoints; j++)
                {
                    var dx = points[i, 0] - points[j, 0];
                    var dy = points[i, 1] - points[j, 1];
                    dist[j] = (float)Math.Sqrt(dx * dx + dy * dy);
                }
                var sorti = (int[])ind.Clone();
                Array.Sort(dist, sorti);
                for (int j = 0; j < NumPoints; j++)
                {
                    distances[i, j] = dist[j];
                    indices[i, j] = sorti[j];
                }
            }
        }

        public void Update(int pipeline, int width, int height, int widthTex, int heightTex)
        {
            // GET OR CREATE CAMERA UNIFORMS FOR program
            var unif = GetUniformBlock(pipeline, name, UniformNames);
            if (unif != null)
            {
                // SET UNIFORM VALUES
                if (unif.Has((int)Names.numPoints))
                {
                    var length = Math.Min(unif[(int)Names.points].length, points.GetLength(0));
                    unif.Set((int)Names.numPoints, new[] { length });
                }

                if (unif.Has((int)Names.points))
                    unif.Set((int)Names.points, points);

                if (unif.Has((int)Names.distances))
                    unif.Set((int)Names.distances, distances);

                if (unif.Has((int)Names.indices))
                    unif.Set((int)Names.indices, indices);

                // UPDATE UNIFORM BUFFER
                unif.Update();
            }

            unif.Bind();
        }

        public Bitmap Visualize()
        {
            var border = 5;
            var resolution = minRadius / 8f;
            float minx = float.MaxValue, miny = float.MaxValue;
            float maxx = float.MinValue, maxy = float.MinValue;
            for (var v = 0; v < points.GetLength(0); v++)
            {
                minx = Math.Min(minx, points[v, 0]);
                miny = Math.Min(miny, points[v, 1]);
                maxx = Math.Max(maxx, points[v, 0]);
                maxy = Math.Max(maxy, points[v, 1]);
            }
            minx = (float)Math.Floor(minx / resolution) - border;
            miny = (float)Math.Floor(miny / resolution) - border;
            maxx = (float)Math.Ceiling(maxx / resolution) + border;
            maxy = (float)Math.Ceiling(maxy / resolution) + border;

            var width = maxx - minx;
            var height = maxy - miny;

            var image = new Bitmap((int)width, (int)height);
            using (var g = Graphics.FromImage(image))
            {
                for (var v = 0; v < points.GetLength(0); v++)
                {
                    g.FillRectangle(Brushes.White, (int)(points[v, 0] / resolution - minx),
                                                   (int)(points[v, 1] / resolution - miny),
                                                   1, 1);
                }
            }

            return image;
        }
    }


    static class PoissonSampler
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
            return Sample(center - new Vector2(radius, radius), center + new Vector2(radius, radius), radius, minimumDistance, pointsPerIteration);
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
                Center = (topLeft + lowerRight) * 0.5f,
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

        static float Distance(Vector2 v0, Vector2 v1)
        {
            return (float)Math.Sqrt(DistanceSquared(v0, v1));
        }

        static float DistanceSquared(Vector2 v0, Vector2 v1)
        {
            return (v1.X - v0.X) * (v1.X - v0.X) + (v1.Y - v0.Y) * (v1.Y - v0.Y);
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
                if (settings.RejectionSqDistance != null && (DistanceSquared(settings.Center, p) > settings.RejectionSqDistance))
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
                (settings.RejectionSqDistance == null || (DistanceSquared(settings.Center, q) <= settings.RejectionSqDistance)))
            {
                var qIndex = Denormalize(q, settings.TopLeft, settings.CellSize);
                var tooClose = false;

                for (var i = (int)Math.Max(0, qIndex.X - 2); i < Math.Min(settings.GridWidth, qIndex.X + 3) && !tooClose; i++)
                    for (var j = (int)Math.Max(0, qIndex.Y - 2); j < Math.Min(settings.GridHeight, qIndex.Y + 3) && !tooClose; j++)
                        if (state.Grid[i, j].HasValue && (Distance(state.Grid[i, j].Value, q) < settings.MinimumDistance))
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

    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.X * b, a.Y * b);
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