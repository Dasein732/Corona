using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Program
{
    public struct Sphere
    {
        public Vector3 Position;
        public float Radius;

        public Sphere(Vector3 position)
        {
            Position = position;
            Radius = 1f;
        }
    }
}