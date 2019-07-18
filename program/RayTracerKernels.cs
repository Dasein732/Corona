using System;
using System.Numerics;
using ILGPU;
using Index = ILGPU.Index;

namespace Program
{
    public static class RayTracerKernels
    {
        public static void Frame(Index2 index, ArrayView2D<Vector4> dataView, int xMax, int yMax)
        {
            float g = index.X / (float)xMax;
            float r = index.Y / (float)yMax;
            dataView[index] = new Vector4(r, g, 0f, 255f);
        }
    }
}