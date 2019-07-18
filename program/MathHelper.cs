using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Program
{
    // Placeholder class, will move to kernel
    public static class MathHelper
    {
        public static Ray Translate(Ray ray, float x, float y, float z)
        {
            var matrix = Matrix4x4.Identity;
            matrix.Translation = new Vector3(x, y, z);

            return new Ray(Vector3.Transform(ray.Origin, matrix), ray.Direction);
        }

        public static Ray Scale(Ray ray, float x, float y, float z)
        {
            var matrix = Matrix4x4.Identity;
            matrix.M11 = x;
            matrix.M22 = y;
            matrix.M33 = z;

            return new Ray(Vector3.Transform(ray.Origin, matrix), Vector3.Transform(ray.Direction, matrix));
        }
    }
}