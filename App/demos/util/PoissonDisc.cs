﻿using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace util
{
    using Commands = Dictionary<string, string[]>;

    class PoissonDisc
    {
        public enum Names
        {
            points,
            radius,
        }

        #region FIELDS
        private string name = "PoissonDisc";
        public int maxSamples = 0;
        public int numRadii = 0;
        public float minRadius = 0f;
        private float[,] samples;
        private int[] radius;
        protected Dictionary<int, Unif> uniform = new Dictionary<int, Unif>();
        protected List<string> errors = new List<string>();
        private static CultureInfo culture = new CultureInfo("en");
        #endregion

        // Properties accessible by ProtoGL
        #region PROPERTIES
        public string Name { get { return name; } set { name = value; } }
        public int MaxSamples { get { return maxSamples; } set { maxSamples = value; } }
        public float MinRadius { get { return minRadius; } set { minRadius = value; } }
        public int NumRadii { get { return numRadii; } set { numRadii = value; } }
        #endregion

        public List<string> GetErrors() { return errors; }

        public PoissonDisc(Commands cmds)
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

            var points = PoissonDiscSampler.Disc(minRadius);

            // SORT POISSON DISK POINTS BY DESCENDING DISTANCE TO EACH OTHER

            maxSamples = maxSamples > 0 ? Math.Min(points.Count, maxSamples) : points.Count;
            List<Vector2> sortedPoints = new List<Vector2>(maxSamples);
            List<float> sortedDist = new List<float>(maxSamples);

            // find center point
            int idx = ClosestPoint(points, new Vector2(0f, 0f));
            sortedPoints.Add(points[idx]);
            sortedDist.Add(float.MaxValue);
            points[idx] = points[points.Count - 1];
            points.RemoveAt(points.Count - 1);

            // sort points
            for (int i = 1; i < maxSamples; i++)
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
            samples = new float[sortedPoints.Count, 2];
            int iter = 0;
            foreach (var point in sortedPoints)
            {
                samples[iter, 0] = point.X;
                samples[iter, 1] = point.Y;
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
                for (; j < maxSamples; j++)
                    if (dist[j] <= r)
                        break;
                // save index in lookup table
                radius[i] = j;
            }
        }

        public void Update(int program, int width, int height, int widthTex, int heightTex)
        {
            // GET OR CREATE CAMERA UNIFORMS FOR program
            Unif unif;
            if (uniform.TryGetValue(program, out unif) == false)
                uniform.Add(program, unif = new Unif(program, name,
                    Names.points, samples, Names.radius, radius));

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
                value = (T)System.Convert.ChangeType(invalue, typeof(T), culture);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region INNER CLASSES
        protected struct Unif
        {
            private int glbuf;
            private int unit;
            private int size;
            private int[] location;
            private int[] length;
            private int[] offset;
            private int[] stride;
            public int Unit { get { return unit; } }
            public IntPtr Size { get { return (IntPtr)size; } }

            public Unif(int program, string name, params object[] nameArrayPairs)
            {
                // get uniform block binding unit and size
                int block = GL.GetUniformBlockIndex(program, name);
                GL.GetActiveUniformBlock(program, block, 
                    ActiveUniformBlockParameter.UniformBlockBinding, out unit);
                GL.GetActiveUniformBlock(program, block, 
                    ActiveUniformBlockParameter.UniformBlockDataSize, out size);
                
                // allocate memory for uniform block uniforms
                string[] names = Enum.GetNames(typeof(Names)).Select(v => name + "." + v).ToArray();
                location = Enumerable.Repeat(-1, names.Length).ToArray();
                length = new int[names.Length];
                offset = new int[names.Length];
                stride = new int[names.Length];

                // get uniform indices in uniform block
                GL.GetUniformIndices(program, names.Length, names, location);

                // get additional information about uniforms
                // like array length, offset and stride
                for (int i = 0; i < location.Length; i++)
                {
                    if (location[i] >= 0)
                    {
                        GL.GetActiveUniforms(program, 1, ref location[i],
                            ActiveUniformParameter.UniformSize, out length[i]);
                        GL.GetActiveUniforms(program, 1, ref location[i],
                            ActiveUniformParameter.UniformOffset, out offset[i]);
                        GL.GetActiveUniforms(program, 1, ref location[i],
                            ActiveUniformParameter.UniformArrayStride, out stride[i]);
                    }
                }

                // allocate GPU memory
                glbuf = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.UniformBuffer, glbuf);

                // allocate CPU memory
                var ptr = Marshal.AllocHGlobal(size);
                for (int i = 0; i < nameArrayPairs.Length; i += 2)
                    Copy((Names)nameArrayPairs[i], (Array)nameArrayPairs[i+1], ptr);

                // copy CPU data to GPU
                GL.BufferData(BufferTarget.UniformBuffer, Size, ptr, BufferUsageHint.DynamicDraw);

                // CPU memory no longer needed
                Marshal.FreeHGlobal(ptr);
            }

            public void Bind()
            {
                GL.BindBufferBase(BufferRangeTarget.UniformBuffer, unit, glbuf);
            }

            public void Delete()
            {
                if (glbuf > 0)
                {
                    GL.DeleteBuffer(glbuf);
                    glbuf = 0;
                }
            }

            private void Copy(Names name, Array array, IntPtr ptr)
            {
                if (location[(int)name] < 0)
                    return;

                // gather some needed information
                int srcStride = Marshal.SizeOf(array.GetType().GetElementType()) * array.GetLength(1);
                int dstStride = stride[(int)name];
                int len = Math.Min(array.GetLength(0), length[(int)name]);

                // convert array to byte array
                byte[] src = new byte[srcStride * array.GetLength(0)];
                Buffer.BlockCopy(array, 0, src, 0, src.Length);

                // stride copy data to new array if necessary
                byte[] dst;
                if (srcStride == dstStride)
                    dst = src;
                else
                {
                    dst = new byte[dstStride * array.GetLength(0)];
                    for (int srci = 0, dsti = 0, skip = dstStride - srcStride; srci < src.Length; dsti += skip)
                        dst[dsti++] = src[srci++];
                }

                // copy to unmanaged memory
                Marshal.Copy(dst, 0, ptr, dstStride * len);
            }
        }

        private static class PoissonDiscSampler
        {
            private static Random rand = new Random();

            public static List<Vector2> Disc(float radius)
            {
                float radiusSq = radius * radius;
                Grid grid = new Grid(radius);
                List<Vector2> points = new List<Vector2>();
                List<Vector2> active = new List<Vector2>();

                // Begin adding points starting with the center
                active.Add(new Vector2(0f, 0f));

                while (active.Count > 0)
                {
                    // Pick a random active sample
                    int i = (int)(rand.NextDouble() * active.Count);
                    int n = points.Count;
                    Vector2 sample = active[i];

                    // Try `k` random candidates between
                    // [radius, 2 * radius] from that sample.
                    const int k = 30;
                    for (int j = 0; j < k; j++)
                    {
                        // See: http://stackoverflow.com/questions/9048095/create-random-number-within-an-annulus/9048443#9048443
                        var r = (float)Math.Sqrt(rand.NextDouble() * 3 * radiusSq + radiusSq);
                        var angle = 2 * Math.PI * rand.NextDouble();
                        Vector2 candidate = sample + r * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

                        // Accept candidates if it's inside the unit disc and
                        // farther than 2 * radius to any existing sample.
                        //if (candidate.Length <= 1f && IsFarEnough(candidate))
                        if (candidate.Length <= 1f && grid.CanInsert(sample) && !grid.HasPoint(sample))
                        {
                            active.Add(candidate);
                            grid.Insert(candidate);
                            points.Add(candidate);
                            break;
                        }
                    }

                    // If we couldn't find a valid candidate after k attempts,
                    // remove this sample from the active samples queue
                    if (n == points.Count)
                    {
                        // move last sample to current sample position
                        active[i] = active[active.Count - 1];
                        // remove last element in the list
                        active.RemoveAt(active.Count - 1);
                    }
                }

                return points;
            }

            private class Grid
            {
                private Vector2[,] grid;
                private float cellSize;
                private float radius;
                private float radiusSq;

                public Grid(float radius)
                {
                    this.radius = radius;
                    radiusSq = radius * radius;
                    cellSize = radius / (float)Math.Sqrt(2);
                    grid = new Vector2[(int)(1 / cellSize), (int)(1 / cellSize)];
                }

                public void Insert(Vector2 sample)
                {
                    Vec2i pos = Disc2Grid(sample);
                    grid[pos.X, pos.Y] = sample;
                }

                public bool CanInsert(Vector2 sample)
                {
                    return CanInsert(Disc2Grid(sample));
                }

                public bool CanInsert(Vec2i pos)
                {
                    return grid[pos.X, pos.Y] == Vector2.Zero;
                }

                public bool HasPoint(Vector2 sample)
                {
                    var pos = Disc2Grid(sample);

                    // 
                    int xmin = Math.Max(pos.X - 2, 0);
                    int ymin = Math.Max(pos.Y - 2, 0);
                    int xmax = Math.Min(pos.X + 2, grid.GetLength(0) - 1);
                    int ymax = Math.Min(pos.Y + 2, grid.GetLength(1) - 1);

                    //
                    for (int y = ymin; y <= ymax; y++)
                    {
                        for (int x = xmin; x <= xmax; x++)
                        {
                            Vector2 s = grid[x, y];
                            if (s != Vector2.Zero)
                            {
                                Vector2 d = s - sample;
                                if (d.X * d.X + d.Y * d.Y < radiusSq)
                                    return true;
                            }
                        }
                    }

                    return false;
                }

                public Vec2i Disc2Grid(Vector2 sample)
                {
                    int x = (int)((sample.X * 0.5f + 0.5f) / cellSize);
                    int y = (int)((sample.Y * 0.5f + 0.5f) / cellSize);
                    x = Math.Min(x, grid.GetLength(0) - 1);
                    y = Math.Min(y, grid.GetLength(1) - 1);
                    return new Vec2i(x, y);
                }

                public struct Vec2i
                {
                    public int X;
                    public int Y;
                    public Vec2i(int x, int y)
                    {
                        X = x;
                        Y = y;
                    }
                }
            }
        }
        #endregion
    }
}