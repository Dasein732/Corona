using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ILGPU;

namespace Program
{
    public struct Ray
    {
        public Vector3 Origin;
        public Vector3 Direction;

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vector3 Position(float tDistance)
        {
            return Position(this, tDistance);
        }

        public static Vector3 Position(Ray ray, float tDistance)
        {
            return ray.Origin + ray.Direction * tDistance;
        }

        public float[] Intersect(Sphere sphere)
        {
            return Intersect(ref this, ref sphere);
        }

        public static float[] Intersect(ref Ray ray, ref Sphere sphere)
        {
            var sphereToRay = ray.Origin - sphere.Position;

            var a = Vector3.Dot(ray.Direction, ray.Direction);
            var b = Vector3.Dot(ray.Direction, sphereToRay) * 2;
            var c = Vector3.Dot(sphereToRay, sphereToRay) - 1;

            var discriminant = b * b - 4 * a * c;

            if(discriminant < 0)
            {
                return new float[0];
            }
            else
            {
                return new float[2]
                {
                    (-b - XMath.Sqrt(discriminant)) / (2 * a),
                    (-b + XMath.Sqrt(discriminant)) / (2 * a)
                };
            }
        }
    }
}