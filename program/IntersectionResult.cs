using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    public struct IntersectionResult
    {
        public float T;
        public Sphere IntersectedEntity;

        public IntersectionResult(float t, ref Sphere intersectedEntity)
        {
            T = t;
            IntersectedEntity = intersectedEntity;
        }
    }
}