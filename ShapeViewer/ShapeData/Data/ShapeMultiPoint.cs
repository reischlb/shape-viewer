using ShapeData.Geometry;

namespace ShapeData.Data
{
    public class ShapeMultiPoint : IShapeData
    {
        public List<Point> Points { get; init; }

        public Dictionary<string, object> Attributes { get; init; } = new();

        public ShapeMultiPoint(List<Point> points)
        {
            Points = points;
        }
    }
}
