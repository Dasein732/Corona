using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Program
{
    public struct Ray
    {
        public Vector4 Origin;
        public Vector4 Direction;

        public Vector4 Position(float tDistance)
        {
            return Position(this, tDistance);
        }

        public static Vector4 Position(Ray ray, float tDistance)
        {
            return ray.Origin + ray.Direction * tDistance;
        }
    }
}