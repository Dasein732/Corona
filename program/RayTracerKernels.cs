using System.Numerics;
using ILGPU;
using Index = ILGPU.Index;

namespace Program
{
    public static class RayTracerKernels
    {
        public static void Frame(Index index, ArrayView<Vector4> dataView, int xMax, int yMax)
        {
            float g = index % (float)xMax / xMax;
            float r = index / (float)yMax / yMax;
            dataView[index] = new Vector4(r, g, 0f, 255f);
        }
    }
}