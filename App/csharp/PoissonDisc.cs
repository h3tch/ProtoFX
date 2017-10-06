using System;
using System.Collections.Generic;
using System.Drawing;
using Commands = System.Collections.Generic.Dictionary<string, string[]>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;

namespace csharp
{
    class PoissonDisc : CsObject
    {
        public enum Names
        {
            numPoints,
            points,
        }

        #region FIELDS
        private string name = "PoissonDisc";
        public int maxPoints = 0;
        public float minRadius = 0f;
        public float[,] points;
        protected Dictionary<int, UniformBlock<Names>> uniform =
            new Dictionary<int, UniformBlock<Names>>();
        #endregion

        // Properties accessible by ProtoGL
        #region PROPERTIES
        public string Name { get { return name; } }
        public int MaxPoints { get { return maxPoints; } }
        public float MinRadius { get { return minRadius; } }
        #endregion

        public PoissonDisc(string name, Commands cmds, GLNames glNames)
        {
            // PARSE COMMAND VALUES SPECIFIED BY THE USER

            this.name = name;
            Convert(cmds, "name", ref this.name);
            Convert(cmds, "maxPoints", ref maxPoints);
            Convert(cmds, "nPoints", ref maxPoints);
            Convert(cmds, "minRadius", ref minRadius);
            Convert(cmds, "minDist", ref minRadius);

            // CREATE POISSON DISK

            points = Disc.Generate(ref maxPoints, ref minRadius);
        }

        public void Update(int pipeline, int width, int height, int widthTex, int heightTex)
        {
            // GET OR CREATE POISSON DISC UNIFORMS FOR program
            UniformBlock<Names> unif;
            if (uniform.TryGetValue(pipeline, out unif) == false)
            {
                uniform.Add(pipeline, unif = new UniformBlock<Names>(pipeline, name));
                // SET UNIFORM VALUES
                unif.Set(Names.numPoints, new[] { points.GetLength(0) });
                unif.Set(Names.points, points);
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

        public void Delete()
        {
            foreach (var u in uniform)
                u.Value.Delete();
        }

        #region INNER CLASSES
        class Disc
        {
            private class DefaultRandom
            {
                public DefaultRandom()
                {
                    rnd = new Random();
                }

                public float RandomFloat()
                {
                    return (float)rnd.NextDouble();
                }

                public int RandomInt(int Max)
                {
                    return rnd.Next(Max);
                }

                private Random rnd;
            };

            private struct Point
            {
                public float x;
                public float y;
                public bool m_Valid;

                public Point(bool valid = false)
                {
                    x = 0;
                    y = 0;
                    m_Valid = valid;
                }

                public Point(float X, float Y)
                {
                    x = X;
                    y = Y;
                    m_Valid = true;
                }

                public bool IsInRectangle()
                {
                    return x >= 0 && y >= 0 && x <= 1 && y <= 1;
                }

                public bool IsInCircle()
                {

                    float fx = x - 0.5f;
                    float fy = y - 0.5f;
                    return ( fx* fx + fy* fy ) <= 0.25f;
                }

                //public float LengthSq() {
                //    return x* x + y* y;
                //}

                //public Point Sub(Point b)
                //{
                //    return new Point(x - b.x, y - b.y);
                //}
            };

            private struct GridPoint
            {
                public int x;
                public int y;

                public GridPoint(int X, int Y)
                {
                    x = X;
                    y = Y;
                }
            };

            private static float GetDistance(Point P1, Point P2 )
            {
                return (float)Math.Sqrt((P1.x - P2.x) * (P1.x - P2.x) + (P1.y - P2.y) * (P1.y - P2.y));
            }

            private static GridPoint ImageToGrid(Point P, float CellSize)
            {
                return new GridPoint((int)(P.x / CellSize), (int)(P.y / CellSize));
            }

            private struct Grid
            {
                public Grid(int W, int H, float CellSize)
                {
                    m_W = W;
                    m_H = H;
                    m_CellSize = CellSize;
                    m_Grid = new Point[m_H][];

                    for (var i = 0; i != m_Grid.Length; i++)
                        m_Grid[i] = new Point[m_W];
                }

                public void Insert(Point P )
                {
                    GridPoint G = ImageToGrid(P, m_CellSize);
                    m_Grid[G.x][G.y] = P;
                }

                public bool IsInNeighbourhood(Point Point, float MinDist, float CellSize)
                {
                    GridPoint G = ImageToGrid(Point, CellSize);

                    // number of adjucent cells to look for neighbour points
                    const int D = 5;

                    // scan the neighbourhood of the point in the grid
                    for (int i = G.x - D; i < G.x + D; i++)
                    {
                        for (int j = G.y - D; j < G.y + D; j++)
                        {
                            if (i >= 0 && i < m_W && j >= 0 && j < m_H)
                            {
                                Point P = m_Grid[i][j];
                                if (P.m_Valid && GetDistance(P, Point) < MinDist)
                                    return true;
                            }
                        }
                    }

                    return false;
                }

                public int m_W;
                public int m_H;
                public float m_CellSize;
                Point[][] m_Grid;
            };

            private static Point PopRandom(List<Point> Points, DefaultRandom Generator)
            {
                int Idx = Generator.RandomInt(Points.Count - 1);
                Point P = Points[Idx];
                Points.RemoveAt(Idx);
                return P;
            }
            
            private static Point GenerateRandomPointAround(Point P, float MinDist, DefaultRandom Generator )
            {
                // start with non-uniform distribution
                float R1 = Generator.RandomFloat();
                float R2 = Generator.RandomFloat();

                // radius should be between MinDist and 2 * MinDist
                float Radius = MinDist * (R1 + 1.0f);

                // random angle
                float Angle = 2 * 3.141592653589f * R2;

                // the new point is generated around the point (x, y)
                float X = (float)(P.x + Radius * Math.Cos(Angle));
                float Y = (float)(P.y + Radius * Math.Sin(Angle));

                return new Point(X, Y);
            }
            
            public static float[,] Generate(ref int NumPoints, ref float MinDist,
                bool Circle = true, int NewPointsCount = 30)
            {
                if (MinDist <= 0.0f && NumPoints <= 0)
                    NumPoints = 2000;
                if (MinDist <= 0.0f)
                    MinDist = (float)Math.Sqrt((float)NumPoints) / NumPoints;
                if (NumPoints <= 0)
                    NumPoints = (int)(1 / (MinDist * MinDist));

                var Generator = new DefaultRandom();
                var SamplePoints = new List<Point>();
                var ProcessList = new List<Point>();

                // create the grid
                float CellSize = MinDist / (float)Math.Sqrt(2.0f);

                int GridW = (int)Math.Ceiling(1.0f / CellSize);
                int GridH = (int)Math.Ceiling(1.0f / CellSize);

                Grid Grid = new Grid(GridW, GridH, CellSize);

                Point FirstPoint;
                do
                {
                    FirstPoint = new Point(Generator.RandomFloat(), Generator.RandomFloat());
                } while (!(Circle ? FirstPoint.IsInCircle() : FirstPoint.IsInRectangle()));

                // update containers
                ProcessList.Add(FirstPoint);
                SamplePoints.Add(FirstPoint);
                Grid.Insert(FirstPoint);

                // generate new points for each point in the queue
                while (ProcessList.Count > 0 && SamplePoints.Count < NumPoints)
                {
                    Point Point = PopRandom(ProcessList, Generator);

                    for (int i = 0; i < NewPointsCount; i++)
                    {
                        Point NewPoint = GenerateRandomPointAround(Point, MinDist, Generator);

                        bool Fits = Circle ? NewPoint.IsInCircle() : NewPoint.IsInRectangle();

                        if (Fits && !Grid.IsInNeighbourhood(NewPoint, MinDist, CellSize))
                        {
                            ProcessList.Add(NewPoint);
                            SamplePoints.Add(NewPoint);
                            Grid.Insert(NewPoint);
                            continue;
                        }
                    }
                }

                var result = new float[SamplePoints.Count, 2];
                for (int i = 0; i < SamplePoints.Count; i++)
                {
                    result[i, 0] = SamplePoints[i].x * 2 - 1;
                    result[i, 1] = SamplePoints[i].y * 2 - 1;
                }

                return result;
            }

        }
        #endregion
    }
}