using System;
using System.Numerics;
using ILGPU;
using Index = ILGPU.Index;

namespace Program
{
    public static class RayTracerKernels
    {
        public static void Frame(GroupedIndex index, ArrayView2D<Vector4> dataView, int xMax, int yMax)
        {
            var idx = index.ComputeGlobalIndex();
            if(idx >= xMax * yMax)
            {
                return;
            }
            int x = idx % xMax;
            int y = idx / xMax;

            float g = x / (float)xMax;
            float r = y / (float)yMax;

            dataView[x, y] = new Vector4(r, g, 0f, 255f);
        }
    }
}