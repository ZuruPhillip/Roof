using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ZURU.Roof.Utility.Maths
{
    // 2D Vector in double
    public struct Vector2D
    {
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector2D(Vector2 vector) : this(vector.X, vector.Y) { }

        public double X { get; }
        public double Y { get; }

        public double Length => Math.Sqrt(X * X + Y * Y);

        public Vector2D Normalize()
            => Length == 0 ? throw new ArgumentException(nameof(Length) + ": Zero length vector cannot be normalized!")
                           : new Vector2D(X / Length, Y / Length);

        public double Dot(Vector2D other) => X * other.X + Y * other.Y;

        public static Vector2D operator -(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2D operator +(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2D operator *(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X * right.X, left.Y * right.Y);
        }

        public Vector2 ToVector2() => new Vector2((float)X, (float)Y);
    }
}
