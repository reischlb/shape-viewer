using ShapeData.Geometry;

namespace ShapeData.Data
{
    public class ShapePoint : IShapeData
    {
        public Point Point { get; init; }

        public Dictionary<string, string> Attributes { get; init; } = new();

        public ShapePoint(Point point)
        {
            Point = point;
        }
    }
}