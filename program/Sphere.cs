using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Program
{
    public struct Sphere
    {
        public Vector3 Origin;
        public float Radius;

        public Sphere(Vector3 position)
        {
            Origin = position;
            Radius = 1f;
        }
    }
}