using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using IntType = OpenTK.Graphics.OpenGL4.VertexAttribIntegerType;
using PointerType = OpenTK.Graphics.OpenGL4.VertexAttribPointerType;

namespace App
{
    class GLVertinput : GLObject
    {
        private List<VertAttr> attributes = new List<VertAttr>();

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        public GLVertinput(Compiler.Block block, Dict scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"vertinput '{Name}'");

            // CREATE OPENGL OBJECT
            glname = GL.GenVertexArray();
            GL.BindVertexArray(glname);

            int numAttr = 0;
            foreach (var cmd in block["attr"])
                Attach(numAttr++, cmd, scene, err | $"command '{cmd.Text}'");

            // if errors occurred throw exception
            if (err.HasErrors)
                throw err;

            // unbind object and check for errors
            GL.BindVertexArray(0);
            if (HasErrorOrGlError(err, block))
                throw err;
        }

        /// <summary>
        /// Parse command line and attach the buffer object
        /// to the specified unit (input stream).
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="cmd"></param>
        /// <param name="scene"></param>
        /// <param name="err"></param>
        private void Attach(int unit, Compiler.Command cmd, Dict scene, CompileException err)
        {
            // check commands for errors
            if (cmd.ArgCount < 3)
            {
                err.Error("Command attr needs at least 3 attributes (e.g. 'attr buff_name float 4')", cmd);
                return;
            }

            // parse command arguments
            string buffname = cmd[0].Text;
            string typename = cmd[1].Text;
            int length = int.Parse(cmd[2].Text);
            int stride = cmd.ArgCount > 3 ? int.Parse(cmd[3].Text) : 0;
            int offset = cmd.ArgCount > 4 ? int.Parse(cmd[4].Text) : 0;
            int divisor = cmd.ArgCount > 5 ? int.Parse(cmd[5].Text) : 0;
            
            if (scene.TryGetValue(buffname, out GLBuffer buff, cmd, err) == false)
            {
                err.Error($"Buffer '{buffname}' could not be found.", cmd);
                return;
            }

            // enable vertex array attribute
            GL.BindBuffer(BufferTarget.ArrayBuffer, buff.glname);
            GL.EnableVertexAttribArray(unit);

            // bind buffer to vertex array attribute
            int type = 0;
            if (Enum.TryParse(typename, true, out VertAttrIntType typei))
                GL.VertexAttribIPointer(unit, length, (IntType)(type = (int)typei), stride, (IntPtr)offset);
            else if (Enum.TryParse(typename, true, out VertAttrType typef))
                GL.VertexAttribPointer(unit, length, (PointerType)(type = (int)typef), false, stride, offset);
            else
                err.Error($"Type '{typename}' is not supported.", cmd);

            if (divisor > 0)
                GL.VertexAttribDivisor(unit, divisor);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // add vertex attribute
            attributes.Add(new VertAttr() {
                buffer = buff,
                type = type,
                length = length,
                stride = stride,
                offset = offset,
                divisor = divisor,
            });
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete()
        {
            base.Delete();
            attributes.Clear();
            if (glname > 0)
            {
                GL.DeleteVertexArray(glname);
                glname = 0;
            }
        }

        /// <summary>
        /// Get list of vertex attribute data.
        /// </summary>
        /// <param name="vertexID"></param>
        /// <param name="instanceID"></param>
        /// <returns></returns>
        public IEnumerable<byte[]> GetVertexData(int vertexID, int instanceID)
        {
            foreach (var attr in attributes)
            {
                int typesize = 0;
                switch (attr.type)
                {
                    case (int)VertAttrIntType.Byte: typesize = 1; break;
                    case (int)VertAttrIntType.UByte: typesize = 1; break;
                    case (int)VertAttrIntType.Short: typesize = 2; break;
                    case (int)VertAttrIntType.UShort: typesize = 2; break;
                    case (int)VertAttrIntType.Int: typesize = 4; break;
                    case (int)VertAttrIntType.UInt: typesize = 4; break;
                    case (int)VertAttrType.Float: typesize = 4; break;
                    case (int)VertAttrType.Double: typesize = 8; break;
                    case (int)VertAttrType.Half: typesize = 2; break;
                    case (int)VertAttrType.Fixed: typesize = 4; break;
                    case (int)VertAttrType.UInt_2_10_10_10: typesize = 1; break;
                    case (int)VertAttrType.Int_2_10_10_10: typesize = 1; break;
                }
                var data = new byte[typesize * attr.length];
                int offset = attr.offset + attr.stride * (attr.divisor == 0 ? vertexID : instanceID / attr.divisor);
                attr.buffer.Read(ref data, offset);
                yield return data;
            }
        }

        #region Vertex Attribute Types

        private struct VertAttr
        {
            public GLBuffer buffer;
            public int type;
            public int length;
            public int stride;
            public int offset;
            public int divisor;
        }

        private enum VertAttrIntType
        {
            Byte = 5120,
            UByte = 5121,
            UnsignedByte = 5121,
            Short = 5122,
            UShort = 5123,
            UnsignedShort = 5123,
            Int = 5124,
            UInt = 5125,
            UnsignedInt = 5125
        }

        private enum VertAttrType
        {
            Float = 5126,
            Double = 5130,
            Half = 5131,
            HalfFloat = 5131,
            Fixed = 5132,
            UInt_2_10_10_10 = 33640,
            UnsignedInt_2_10_10_10 = 33640,
            Int_2_10_10_10 = 36255
        }

        #endregion
    }
}
