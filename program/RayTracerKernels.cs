using System;
using System.Numerics;
using ILGPU;
using Index = ILGPU.Index;

namespace Program
{
    public static class RayTracerKernels
    {
        public static void Frame(Index2 index, ArrayView2D<Vector4> dataView, Index2 canvasSize)
        {
            float g = index.X / (float)canvasSize.X;
            float r = index.Y / (float)canvasSize.Y;
            dataView[index] = new Vector4(r, g, 0f, 255f);
        }
    }
}