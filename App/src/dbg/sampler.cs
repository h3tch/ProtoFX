using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.debug
{
    class sampler1D
    {
        private int id;
        public sampler1D() { id = 0; }
        private sampler1D(int id) { this.id = id; }
        public static implicit operator int (sampler1D sampler) { return sampler.id; }
        public static implicit operator sampler1D(int sampler) { return new sampler1D(sampler); }
    }
    class sampler2D
    {
        private int id;
        public sampler2D() { id = 0; }
        private sampler2D(int id) { this.id = id; }
        public static implicit operator int (sampler2D sampler) { return sampler.id; }
        public static implicit operator sampler2D(int sampler) { return new sampler2D(sampler); }
    }
    class sampler3D
    {
        private int id;
        public sampler3D() { id = 0; }
        private sampler3D(int id) { this.id = id; }
        public static implicit operator int (sampler3D sampler) { return sampler.id; }
        public static implicit operator sampler3D(int sampler) { return new sampler3D(sampler); }
    }
    class samplerCube
    {
        private int id;
        public samplerCube() { id = 0; }
        private samplerCube(int id) { this.id = id; }
        public static implicit operator int (samplerCube sampler) { return sampler.id; }
        public static implicit operator samplerCube(int sampler) { return new samplerCube(sampler); }
    }
    class samplerBuffer
    {
        private int id;
        public samplerBuffer() { id = 0; }
        private samplerBuffer(int id) { this.id = id; }
        public static implicit operator int (samplerBuffer sampler) { return sampler.id; }
        public static implicit operator samplerBuffer(int sampler) { return new samplerBuffer(sampler); }
    }
    class sampler1DArray
    {
        private int id;
        public sampler1DArray() { id = 0; }
        private sampler1DArray(int id) { this.id = id; }
        public static implicit operator int (sampler1DArray sampler) { return sampler.id; }
        public static implicit operator sampler1DArray(int sampler) { return new sampler1DArray(sampler); }
    }
    class sampler2DArray
    {
        private int id;
        public sampler2DArray() { id = 0; }
        private sampler2DArray(int id) { this.id = id; }
        public static implicit operator int (sampler2DArray sampler) { return sampler.id; }
        public static implicit operator sampler2DArray(int sampler) { return new sampler2DArray(sampler); }
    }
    class samplerCubeArray
    {
        private int id;
        public samplerCubeArray() { id = 0; }
        private samplerCubeArray(int id) { this.id = id; }
        public static implicit operator int (samplerCubeArray sampler) { return sampler.id; }
        public static implicit operator samplerCubeArray(int sampler) { return new samplerCubeArray(sampler); }
    }
    class sampler2DRect
    {
        private int id;
        public sampler2DRect() { id = 0; }
        private sampler2DRect(int id) { this.id = id; }
        public static implicit operator int (sampler2DRect sampler) { return sampler.id; }
        public static implicit operator sampler2DRect(int sampler) { return new sampler2DRect(sampler); }
    }
    class isampler1D
    {
        private int id;
        public isampler1D() { id = 0; }
        private isampler1D(int id) { this.id = id; }
        public static implicit operator int (isampler1D sampler) { return sampler.id; }
        public static implicit operator isampler1D(int sampler) { return new isampler1D(sampler); }
    }
    class isampler2D
    {
        private int id;
        public isampler2D() { id = 0; }
        private isampler2D(int id) { this.id = id; }
        public static implicit operator int (isampler2D sampler) { return sampler.id; }
        public static implicit operator isampler2D(int sampler) { return new isampler2D(sampler); }
    }
    class isampler3D
    {
        private int id;
        public isampler3D() { id = 0; }
        private isampler3D(int id) { this.id = id; }
        public static implicit operator int (isampler3D sampler) { return sampler.id; }
        public static implicit operator isampler3D(int sampler) { return new isampler3D(sampler); }
    }
    class isamplerCube
    {
        private int id;
        public isamplerCube() { id = 0; }
        private isamplerCube(int id) { this.id = id; }
        public static implicit operator int (isamplerCube sampler) { return sampler.id; }
        public static implicit operator isamplerCube(int sampler) { return new isamplerCube(sampler); }
    }
    class isamplerBuffer
    {
        private int id;
        public isamplerBuffer() { id = 0; }
        private isamplerBuffer(int id) { this.id = id; }
        public static implicit operator int (isamplerBuffer sampler) { return sampler.id; }
        public static implicit operator isamplerBuffer(int sampler) { return new isamplerBuffer(sampler); }
    }
    class isampler1DArray
    {
        private int id;
        public isampler1DArray() { id = 0; }
        private isampler1DArray(int id) { this.id = id; }
        public static implicit operator int (isampler1DArray sampler) { return sampler.id; }
        public static implicit operator isampler1DArray(int sampler) { return new isampler1DArray(sampler); }
    }
    class isampler2DArray
    {
        private int id;
        public isampler2DArray() { id = 0; }
        private isampler2DArray(int id) { this.id = id; }
        public static implicit operator int (isampler2DArray sampler) { return sampler.id; }
        public static implicit operator isampler2DArray(int sampler) { return new isampler2DArray(sampler); }
    }
    class isamplerCubeArray
    {
        private int id;
        public isamplerCubeArray() { id = 0; }
        private isamplerCubeArray(int id) { this.id = id; }
        public static implicit operator int (isamplerCubeArray sampler) { return sampler.id; }
        public static implicit operator isamplerCubeArray(int sampler) { return new isamplerCubeArray(sampler); }
    }
    class isampler2DRect
    {
        private int id;
        public isampler2DRect() { id = 0; }
        private isampler2DRect(int id) { this.id = id; }
        public static implicit operator int (isampler2DRect sampler) { return sampler.id; }
        public static implicit operator isampler2DRect(int sampler) { return new isampler2DRect(sampler); }
    }
    class usampler1D
    {
        private int id;
        public usampler1D() { id = 0; }
        private usampler1D(int id) { this.id = id; }
        public static implicit operator int (usampler1D sampler) { return sampler.id; }
        public static implicit operator usampler1D(int sampler) { return new usampler1D(sampler); }
    }
    class usampler2D
    {
        private int id;
        public usampler2D() { id = 0; }
        private usampler2D(int id) { this.id = id; }
        public static implicit operator int (usampler2D sampler) { return sampler.id; }
        public static implicit operator usampler2D(int sampler) { return new usampler2D(sampler); }
    }
    class usampler3D
    {
        private int id;
        public usampler3D() { id = 0; }
        private usampler3D(int id) { this.id = id; }
        public static implicit operator int (usampler3D sampler) { return sampler.id; }
        public static implicit operator usampler3D(int sampler) { return new usampler3D(sampler); }
    }
    class usamplerCube
    {
        private int id;
        public usamplerCube() { id = 0; }
        private usamplerCube(int id) { this.id = id; }
        public static implicit operator int (usamplerCube sampler) { return sampler.id; }
        public static implicit operator usamplerCube(int sampler) { return new usamplerCube(sampler); }
    }
    class usamplerBuffer
    {
        private int id;
        public usamplerBuffer() { id = 0; }
        private usamplerBuffer(int id) { this.id = id; }
        public static implicit operator int (usamplerBuffer sampler) { return sampler.id; }
        public static implicit operator usamplerBuffer(int sampler) { return new usamplerBuffer(sampler); }
    }
    class usampler1DArray
    {
        private int id;
        public usampler1DArray() { id = 0; }
        private usampler1DArray(int id) { this.id = id; }
        public static implicit operator int (usampler1DArray sampler) { return sampler.id; }
        public static implicit operator usampler1DArray(int sampler) { return new usampler1DArray(sampler); }
    }
    class usampler2DArray
    {
        private int id;
        public usampler2DArray() { id = 0; }
        private usampler2DArray(int id) { this.id = id; }
        public static implicit operator int (usampler2DArray sampler) { return sampler.id; }
        public static implicit operator usampler2DArray(int sampler) { return new usampler2DArray(sampler); }
    }
    class usamplerCubeArray
    {
        private int id;
        public usamplerCubeArray() { id = 0; }
        private usamplerCubeArray(int id) { this.id = id; }
        public static implicit operator int (usamplerCubeArray sampler) { return sampler.id; }
        public static implicit operator usamplerCubeArray(int sampler) { return new usamplerCubeArray(sampler); }
    }
    class usampler2DRect
    {
        private int id;
        public usampler2DRect() { id = 0; }
        private usampler2DRect(int id) { this.id = id; }
        public static implicit operator int (usampler2DRect sampler) { return sampler.id; }
        public static implicit operator usampler2DRect(int sampler) { return new usampler2DRect(sampler); }
    }
    class sampler1DShadow
    {
        private int id;
        public sampler1DShadow()
        { id = 0; }
        private sampler1DShadow(int id)
        { this.id = id; }
        public static implicit operator int (sampler1DShadow sampler)
        { return sampler.id; }
        public static implicit operator sampler1DShadow(int sampler)
        { return new sampler1DShadow(sampler); }
    }
    class sampler2DShadow
    {
        private int id;
        public sampler2DShadow()
        { id = 0; }
        private sampler2DShadow(int id)
        { this.id = id; }
        public static implicit operator int (sampler2DShadow sampler)
        { return sampler.id; }
        public static implicit operator sampler2DShadow(int sampler)
        { return new sampler2DShadow(sampler); }
    }
    class samplerCubeShadow
    {
        private int id;
        public samplerCubeShadow()
        { id = 0; }
        private samplerCubeShadow(int id)
        { this.id = id; }
        public static implicit operator int (samplerCubeShadow sampler)
        { return sampler.id; }
        public static implicit operator samplerCubeShadow(int sampler)
        { return new samplerCubeShadow(sampler); }
    }
    class sampler1DArrayShadow
    {
        private int id;
        public sampler1DArrayShadow()
        { id = 0; }
        private sampler1DArrayShadow(int id)
        { this.id = id; }
        public static implicit operator int (sampler1DArrayShadow sampler)
        { return sampler.id; }
        public static implicit operator sampler1DArrayShadow(int sampler)
        { return new sampler1DArrayShadow(sampler); }
    }
    class sampler2DArrayShadow
    {
        private int id;
        public sampler2DArrayShadow()
        { id = 0; }
        private sampler2DArrayShadow(int id)
        { this.id = id; }
        public static implicit operator int (sampler2DArrayShadow sampler)
        { return sampler.id; }
        public static implicit operator sampler2DArrayShadow(int sampler)
        { return new sampler2DArrayShadow(sampler); }
    }
    class sampler2DRectShadow
    {
        private int id;
        public sampler2DRectShadow() { id = 0; }
        private sampler2DRectShadow(int id) { this.id = id; }
        public static implicit operator int (sampler2DRectShadow sampler) { return sampler.id; }
        public static implicit operator sampler2DRectShadow(int sampler) { return new sampler2DRectShadow(sampler); }
    }
    class samplerCubeArrayShadow
    {
        private int id;
        public samplerCubeArrayShadow() { id = 0; }
        private samplerCubeArrayShadow(int id) { this.id = id; }
        public static implicit operator int (samplerCubeArrayShadow sampler) { return sampler.id; }
        public static implicit operator samplerCubeArrayShadow(int sampler) { return new samplerCubeArrayShadow(sampler); }
    }
}
