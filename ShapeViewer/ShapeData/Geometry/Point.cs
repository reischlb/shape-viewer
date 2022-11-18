using System.Runtime.CompilerServices;

namespace ShapeData.Geometry
{
    public class Point
    {
        public double X { get; init; }

        public double Y { get; init; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

       public static bool operator ==(Point a, Point b)
       {
           return a.X == b.X && a.Y == b.Y;
       }
       
       public static bool operator !=(Point a, Point b)
       {
           return !(a == b);
       }

        public override bool Equals(object? obj)
        {
            return obj is Point point &&
                   X == point.X &&
                   Y == point.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
