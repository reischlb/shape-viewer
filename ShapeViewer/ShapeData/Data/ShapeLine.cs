using ShapeData.Geometry;

namespace ShapeData.Data
{
    public class ShapeLine : IShapeData
    {
        public Line Line { get; init; }

        public Dictionary<string, string> Attributes { get; init; } = new();

        public ShapeLine(Line line)
        {
            Line = line;
        }
    }
}
