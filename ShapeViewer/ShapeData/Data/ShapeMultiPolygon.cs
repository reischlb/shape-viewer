using ShapeData.Geometry;

namespace ShapeData.Data
{
    public class ShapeMultiPolygon : IShapeData
    {

        public List<Polygon> Polygons { get; init; }

        public Dictionary<string, object> Attributes { get; init; } = new();

        public ShapeMultiPolygon(List<Polygon> polygons)
        {
            Polygons = polygons;
        }
    }
}
