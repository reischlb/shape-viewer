using DotSpatial.Data;
using NetTopologySuite.Geometries;
using ShapeData.Data;
using System.Data;
using System.Diagnostics;
using SDG = ShapeData.Geometry;

namespace ShapeLoader.Loaders
{
    public static class SHPLoader
    {
        public static List<IShapeData> LoadSHP(FileInfo shpFile) =>
            GetShapeDatasFromShapeFile(FeatureSet.Open(shpFile.FullName));

        public static List<IShapeData> GetShapeDatasFromShapeFile(IFeatureSet shapeFile)
        {

            var datas = new List<IShapeData>();

            var zippedDatas = shapeFile.GetZippedAttributes();

            var header = shapeFile.GetAttributeHeaders();

            foreach (var item in zippedDatas)
            {
                var attrs = ZipObjectAttributes(header, item.Attributes);

                switch (item.Geometry.OgcGeometryType)
                {
                    case OgcGeometryType.Polygon:
                        datas.Add(CreateShapePolygonFromSimplePolygon(item.Geometry, attrs));
                        break;
                    case OgcGeometryType.MultiPolygon:
                        datas.Add(CreateShapePolygonFromMultiPolygon(item.Geometry, attrs));
                        break;
                    case OgcGeometryType.Point:
                        datas.Add(CreateShapePointFromPoint(item.Geometry, attrs));
                        break;
                    case OgcGeometryType.MultiPoint:
                        datas.Add(CreateShapePointFromMultiPoint(item.Geometry, attrs));
                        break;
                    case OgcGeometryType.LineString:
                        datas.Add(CreateShapeLineFromLineString(item.Geometry, attrs));
                        break;

                    default:
                        break;
                }
            }
            return datas;

        }

        private static ShapePoint CreateShapePointFromPoint(Geometry polygonGeometry, Dictionary<string, object> attributes)
        {
            return new(new(polygonGeometry.Coordinate.X, polygonGeometry.Coordinate.Y)) { Attributes = attributes };
        }

        private static ShapeMultiPoint CreateShapePointFromMultiPoint(Geometry polygonGeometry, Dictionary<string, object> attributes)
        {
            return new(GetCoordinatesFromGeometry(polygonGeometry)) { Attributes = attributes };
        }

        private static ShapeLine CreateShapeLineFromLineString(Geometry polygonGeometry, Dictionary<string, object> attributes)
        {
            return new(new(GetCoordinatesFromGeometry(polygonGeometry))) { Attributes = attributes };
        }


        private static ShapePolygon CreateShapePolygonFromSimplePolygon(Geometry polygonGeometry, Dictionary<string, object> attributes)
        {
            List<SDG.Point> points = GetCoordinatesFromGeometry(polygonGeometry);

            return new ShapePolygon(new SDG.Polygon(points)) { Attributes = attributes };
        }

        private static List<SDG.Point> GetCoordinatesFromGeometry(Geometry polygonGeometry)
        {
            List<SDG.Point> points = new();
            points.AddRange(polygonGeometry.Coordinates.Select(coordinate => new SDG.Point(coordinate.X, coordinate.Y)));
            return points;
        }

        private static ShapeMultiPolygon CreateShapePolygonFromMultiPolygon(Geometry polygonGeometry, Dictionary<string, object> attributes)
        {
            List<SDG.Point> points = new();
            points.AddRange(polygonGeometry.Coordinates.Select(coordinate => new SDG.Point(coordinate.X, coordinate.Y)));

            List<SDG.Polygon> polygons = new();
            List<SDG.Point> actualPolygonPoints = new();
            foreach (var point in points)
            {
                if (actualPolygonPoints.Count == 0)
                {
                    actualPolygonPoints.Add(point);
                    continue;
                }
                actualPolygonPoints.Add(point);
                if (actualPolygonPoints[0] == point)
                {
                    polygons.Add(new SDG.Polygon(actualPolygonPoints));
                    actualPolygonPoints = new();
                }
            }
            return new ShapeMultiPolygon(polygons) { Attributes = attributes };
        }

        private static Dictionary<string, object> ZipObjectAttributes(IEnumerable<string> columns, object[] row)
        {
            var attrs = columns.Zip(row, (header, attr) => new { Header = header, Attr = attr });
            return attrs.ToDictionary(keySelector: tuple => tuple.Header, elementSelector: tuple => tuple.Attr);
        }
    }
}
