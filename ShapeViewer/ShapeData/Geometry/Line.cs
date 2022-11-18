using System.Diagnostics;

namespace ShapeData.Geometry
{
    public class Line
    {
        public IReadOnlyList<Point> Points { get; init; }

        public Line(IReadOnlyList<Point> points)
        {
            Points = points;
            Debug.Assert(Points[0] == Points[Points.Count - 1]);
        }
    }
}
