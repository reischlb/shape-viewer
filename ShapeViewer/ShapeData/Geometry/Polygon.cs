namespace ShapeData.Geometry
{
    public class Polygon
    {
        public IReadOnlyList<Line> Lines { get; init; }
        public Polygon(IReadOnlyList<Line> lines)
        {
            Lines = lines;
        }
    }
}
