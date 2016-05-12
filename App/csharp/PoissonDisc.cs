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

            points = Disc.Generate(maxPoints, minRadius);
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

        #region INNER CLASSES
        class Disc
        {
            private class DefaultPRNG
            {
                public DefaultPRNG()
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

            private struct sPoint
            {
                public float x;
                public float y;
                public bool m_Valid;

                public sPoint(bool valid = false)
                {
                    x = 0;
                    y = 0;
                    m_Valid = valid;
                }

                public sPoint(float X, float Y)
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

                public float lengthSq() {
                    return x* x + y* y;
                }

                public sPoint Sub(sPoint b)
                {
                    return new sPoint(x - b.x, y - b.y);
                }
            };

            private struct sGridPoint
            {
                public int x;
                public int y;

                public sGridPoint(int X, int Y)
                {
                    x = X;
                    y = Y;
                }
            };

            private static float GetDistance(sPoint P1, sPoint P2 )
            {
                return (float)Math.Sqrt((P1.x - P2.x) * (P1.x - P2.x) + (P1.y - P2.y) * (P1.y - P2.y));
            }

            private static sGridPoint ImageToGrid(sPoint P, float CellSize)
            {
                return new sGridPoint((int)(P.x / CellSize), (int)(P.y / CellSize));
            }

            private struct sGrid
            {
                public sGrid(int W, int H, float CellSize)
                {
                    m_W = W;
                    m_H = H;
                    m_CellSize = CellSize;
                    m_Grid = new sPoint[m_H][];

                    for (var i = 0; i != m_Grid.Length; i++)
                        m_Grid[i] = new sPoint[m_W];
                }

                public void Insert(sPoint P )
                {
                    sGridPoint G = ImageToGrid(P, m_CellSize);
                    m_Grid[G.x][G.y] = P;
                }

                public bool IsInNeighbourhood(sPoint Point, float MinDist, float CellSize)
                {
                    sGridPoint G = ImageToGrid(Point, CellSize);

                    // number of adjucent cells to look for neighbour points
                    const int D = 5;

                    // scan the neighbourhood of the point in the grid
                    for (int i = G.x - D; i < G.x + D; i++)
                    {
                        for (int j = G.y - D; j < G.y + D; j++)
                        {
                            if (i >= 0 && i < m_W && j >= 0 && j < m_H)
                            {
                                sPoint P = m_Grid[i][j];
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
                sPoint[][] m_Grid;
            };

            private static sPoint PopRandom(List<sPoint> Points, DefaultPRNG Generator)
            {
                int Idx = Generator.RandomInt(Points.Count - 1);
                sPoint P = Points[Idx];
                Points.RemoveAt(Idx);
                return P;
            }
            
            private static sPoint GenerateRandomPointAround(sPoint P, float MinDist, DefaultPRNG Generator )
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

                return new sPoint(X, Y);
            }
            
            public static float[,] Generate(int NumPoints = 0, float MinDist = -1f,
                bool Circle = true, int NewPointsCount = 30)
            {
                if (MinDist <= 0.0f && NumPoints <= 0)
                    NumPoints = 2000;
                if (MinDist <= 0.0f)
                    MinDist = (float)Math.Sqrt((float)NumPoints) / NumPoints;
                if (NumPoints <= 0)
                    NumPoints = (int)(1 / (MinDist * MinDist));

                var Generator = new DefaultPRNG();
                var SamplePoints = new List<sPoint>();
                var ProcessList = new List<sPoint>();

                // create the grid
                float CellSize = MinDist / (float)Math.Sqrt(2.0f);

                int GridW = (int)Math.Ceiling(1.0f / CellSize);
                int GridH = (int)Math.Ceiling(1.0f / CellSize);

                sGrid Grid = new sGrid(GridW, GridH, CellSize);

                sPoint FirstPoint;
                do
                {
                    FirstPoint = new sPoint(Generator.RandomFloat(), Generator.RandomFloat());
                } while (!(Circle ? FirstPoint.IsInCircle() : FirstPoint.IsInRectangle()));

                // update containers
                ProcessList.Add(FirstPoint);
                SamplePoints.Add(FirstPoint);
                Grid.Insert(FirstPoint);

                // generate new points for each point in the queue
                while (ProcessList.Count > 0 && SamplePoints.Count < NumPoints)
                {
                    sPoint Point = PopRandom(ProcessList, Generator);

                    for (int i = 0; i < NewPointsCount; i++)
                    {
                        sPoint NewPoint = GenerateRandomPointAround(Point, MinDist, Generator);

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