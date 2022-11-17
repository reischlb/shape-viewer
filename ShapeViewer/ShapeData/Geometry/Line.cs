namespace ShapeData.Geometry
{
    public class Line
    {
        public Point Start { get; init; }

        public Point End { get; init; }

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }
}
