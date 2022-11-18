﻿using System.Diagnostics;

namespace ShapeData.Geometry
{
    public class Polygon
    {
        public IReadOnlyList<Point> Points { get; init; }

        public Polygon(IReadOnlyList<Point> points)
        {
            Points = points;
            Debug.Assert(Points[0] == Points[Points.Count - 1]);
        }
    }
}
