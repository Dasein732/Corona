using System;
using System.Collections.Generic;
using System.Linq;
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

        public IntersectionResult[] Intersect(Sphere sphere)
        {
            return Intersect(ref this, ref sphere);
        }

        public static IntersectionResult[] Intersect(ref Ray ray, ref Sphere sphere)
        {
            var sphereToRay = ray.Origin - sphere.Origin;

            // normalize direction before passing it to intersect?
            var a = Vector3.Dot(ray.Direction, ray.Direction);
            var b = Vector3.Dot(ray.Direction, sphereToRay) * 2;
            var c = Vector3.Dot(sphereToRay, sphereToRay) - 1;

            var discriminant = b * b - 4 * a * c;

            if(discriminant < 0)
            {
                return new IntersectionResult[0];
            }
            else
            {
                return new IntersectionResult[2]
                {
                    new IntersectionResult((-b - XMath.Sqrt(discriminant)) / (2 * a), ref sphere),
                    new IntersectionResult((-b + XMath.Sqrt(discriminant)) / (2 * a), ref sphere)
                };
            }
        }

        // TODO implement in the kernel
        public static IntersectionResult[] Aggregate(IntersectionResult[] left, IntersectionResult[] right)
        {
            return left.Concat(right).ToArray();
        }

        public static IntersectionResult ClosestRayHit(IntersectionResult[] results)
        {
            var tmp = new IntersectionResult();
            tmp.T = float.MaxValue;

            for(int i = 0; i < results.Length; i++)
            {
                if(results[i].T > 0 && results[i].T < tmp.T)
                {
                    tmp.T = results[i].T;
                    tmp.IntersectedEntity = results[i].IntersectedEntity;
                }
            }

            if(tmp.T < float.MaxValue)
            {
                return tmp;
            }

            return new IntersectionResult();
        }
    }
}