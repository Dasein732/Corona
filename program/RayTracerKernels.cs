using System.Numerics;
using ILGPU;
using Index = ILGPU.Index;

namespace Program
{
    public static class RayTracerKernels
    {
        public static void Frame(GroupedIndex index, ArrayView<Vector4> dataView, int xMax, int yMax)
        {
            var idx = index.ComputeGlobalIndex();

            float g = idx % (float)xMax / xMax;
            float r = idx / (float)yMax / yMax;
            dataView[idx] = new Vector4(r, g, 0f, 255f);
        }
    }
}