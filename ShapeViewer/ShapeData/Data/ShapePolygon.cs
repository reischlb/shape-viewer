using ShapeData.Geometry;

namespace ShapeData.Data
{
    public class ShapePolygon : IShapeData
    {
        public Polygon Polygon { get; init; }

        public Dictionary<string, string> Attributes { get; init; } = new();

        public ShapePolygon(Polygon polygon)
        {
            Polygon = polygon;
        }
    }
}
