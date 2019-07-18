using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Program;
using Xunit;

namespace Tests
{
    public class RayTests
    {
        [Fact]
        public void New_Ray_Default_Constructor_Initializes_All_Values_To_Zero()
        {
            var ray = new Ray();

            var actual = new List<Vector3> { ray.Origin, ray.Direction };

            Assert.All(actual, testCase => Assert.Equal(Vector3.Zero, testCase));
        }

        [Theory]
        [MemberData(nameof(RayInitializationData), 5)]
        public void Ray_Initialized_With_Parameters_Has_Those_Values_As_Origin_And_Direction(Vector3 orig, Vector3 dir)
        {
            var ray = new Ray(orig, dir);

            var actual = (ray.Origin, ray.Direction);

            Assert.Equal((orig, dir), actual);
        }

        [Theory]
        [MemberData(nameof(RayPositionData), 5)]
        public static void On_Ray_Position_Calculation_Get_Correct_Result(Ray ray, float t)
        {
            var actual = ray.Position(t);

            var expected = ray.Origin + ray.Direction * t;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(RayIntersectionData), 5)]
        public static void On_Ray_Intersection_Correct_Intersection_Is_Calculated(Ray ray, Sphere sphere, IntersectionResult[] expected)
        {
            var actual = Ray.Intersect(ref ray, ref sphere);

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> RayInitializationData(int numTests)
        {
            var testData = new List<object[]> {
                new object[]{Vector3.Zero, Vector3.Zero },
                new object[]{Vector3.One, Vector3.Zero },
                new object[]{Vector3.Zero, Vector3.One },
                new object[]{new Vector3(1f, 2f, 3f), new Vector3(4f, 5f, 6f) },
                new object[]{new Vector3(81f, 52f, 13f), new Vector3(32f, 55f, 62f) },
            };

            return testData.Take(numTests);
        }

        public static IEnumerable<object[]> RayPositionData(int numTests)
        {
            var testData = new List<object[]> {
                new object[]{new Ray(Vector3.Zero, Vector3.One), 0f },
                new object[]{new Ray(Vector3.Zero, Vector3.One), 1f },
                new object[]{new Ray(new Vector3(1f, 2f, 3f), Vector3.One), -1f },
                new object[]{new Ray(Vector3.Zero, new Vector3(7f, 5f, 3f)), 3f },
                new object[]{new Ray(new Vector3(4f, 8f, 12f), new Vector3(11f, 24f, 35f)), -10f },
            };

            return testData.Take(numTests);
        }

        public static IEnumerable<object[]> RayIntersectionData(int numTests)
        {
            var sphere = new Sphere(Vector3.Zero);

            var testData = new List<object[]> {
                new object[]{new Ray(new Vector3(0f, 0f, -5f), new Vector3(0f, 0f, 1f)), sphere, new IntersectionResult[] {new IntersectionResult(4f, ref sphere), new IntersectionResult(6f, ref sphere) } },
                new object[]{new Ray(new Vector3(0f, 1f, -5f), new Vector3(0f, 0f, 1f)), sphere, new IntersectionResult[] { new IntersectionResult(5f, ref sphere), new IntersectionResult(5f, ref sphere) } },
                new object[]{new Ray(new Vector3(0f, 2f, -5f), new Vector3(0f, 0f, 1f)), sphere, new IntersectionResult[] { } },
                new object[]{new Ray(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 1f)), sphere, new IntersectionResult[] { new IntersectionResult(-1f, ref sphere), new IntersectionResult(1f, ref sphere) } },
                new object[]{new Ray(new Vector3(0f, 0f, 5f), new Vector3(0f, 0f, 1f)), sphere, new IntersectionResult[] { new IntersectionResult(-6f, ref sphere), new IntersectionResult(-4f, ref sphere) } },
            };

            return testData.Take(numTests);
        }
    }
}