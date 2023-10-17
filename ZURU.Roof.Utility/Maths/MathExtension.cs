using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ZURU.Roof.Utility.Maths
{
    public static class MathExtensions
    {
        private static readonly float floatTolerance = 1e-1f;
        private static readonly double doubleTolerance = 1e-1;

        public static Vector2 Round(this Vector2 vector2, int dp = 4)
        {
            if (vector2 == null)
            {
                throw new ArgumentNullException("Vector2 is null");
            }
            return new Vector2(MathF.Round(vector2.X, dp), MathF.Round(vector2.Y, dp));
        }

        public static Vector2D ToVector2D(this Vector2 vector) => new Vector2D(vector.X, vector.Y);

        public static bool CloseTo(this Vector2 vector1, Vector2 vector2)
        {
            return vector1.X.CloseTo(vector2.X)
                && vector1.Y.CloseTo(vector2.Y);
        }

        public static bool CloseTo(this Vector3 vector1, Vector3 vector2)
        {
            return vector1.X.CloseTo(vector2.X)
                && vector1.Y.CloseTo(vector2.Y)
                && vector1.Z.CloseTo(vector2.Z);
        }

        public static Vector3 Round(this Vector3 vector3, int dp = 4)
        {
            if (vector3 == null)
            {
                throw new ArgumentNullException("Vector3 is null");
            }
            return new Vector3(MathF.Round(vector3.X, dp), MathF.Round(vector3.Y, dp), MathF.Round(vector3.Z, dp));
        }
        public static Quaternion Round(this Quaternion q, int dp = 7)
        {
            if (q == null)
            {
                throw new ArgumentNullException("Quaternion is null");
            }
            q.W = MathF.Round(q.W, dp);
            q.X = MathF.Round(q.X, dp);
            q.Y = MathF.Round(q.Y, dp);
            q.Z = MathF.Round(q.Z, dp);
            return q;
        }
        public static float ToRadians(this float val)
        {
            return (MathF.PI / 180) * val;
        }
        public static float ToAngle(this float val)
        {
            return (180 / MathF.PI) * val;
        }

        public static float Round(this float f, int digits)
        {
            return MathF.Round(f, digits);
        }

        public static bool CloseTo(this float a, float b)
        {
            return MathF.Abs(a - b) < floatTolerance;
        }

        public static bool CloseTo(this double a, double b)
        {
            return Math.Abs(a - b) < doubleTolerance;
        }

        public static int Dcmp(this float x)
        {
            if (MathF.Abs(x) < floatTolerance) return 0;
            return x < 0 ? -1 : 1;
        }

        public static int Dcmp(this double x)
        {
            if (Math.Abs(x) < doubleTolerance) return 0;
            return x < 0 ? -1 : 1;
        }
    }
}
