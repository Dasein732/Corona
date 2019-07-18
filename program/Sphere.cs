using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Program
{
    public struct Sphere
    {
        public Vector4 Position;
        public float radius;

        public Sphere(Vector4 position)
        {
            Position = position;
            Position.W = 1f;
            radius = 1f;
        }

        public static Sphere Instantiate(Vector4 position)
        {
            return new Sphere(position);
        }
    }
}