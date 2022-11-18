using DotSpatial.Data;
using NetTopologySuite.Geometries;
using ShapeData.Data;
using System.Data;
using SDG = ShapeData.Geometry;

namespace ShapeLoader.Loaders
{
    public static class SHPLoader
    {
        public static List<IShapeData> LoadSHP(FileInfo shpFile) =>
            GetShapePolygonsFromShapeFile(FeatureSet.Open(shpFile.FullName));

        public static List<IShapeData> GetShapePolygonsFromShapeFile(IFeatureSet shapefile)
        {
            var polygons = new List<IShapeData>();

            if (shapefile.FeatureType is FeatureType.Polygon &&
                shapefile is PolygonShapefile polygonFile)
            {
                return CollectPolygons(polygonFile);
            }
            return polygons;
        }

        private static List<IShapeData> CollectPolygons(PolygonShapefile shapePoint)
        {
            var polygons = new List<IShapeData>();
            var geometries = shapePoint.Features.Select(feature => feature.Geometry);
            var attributes = from DataRow row in shapePoint.DataTable.Rows
                             select row.ItemArray;

            var zippedPolygons = geometries.Zip(attributes, (geom, attrs) => new { Geometry = geom, Attributes = attrs });

            var header = from DataColumn column in shapePoint.DataTable.Columns
                         select column.ColumnName;

            foreach (var item in zippedPolygons)
            {
                var attrs = ZipObjectAttributes(header, item.Attributes);

                switch (item.Geometry.OgcGeometryType)
                {
                    case OgcGeometryType.Polygon:
                        polygons.Add(CreateShapePolygonFromSimplePolygon(item.Geometry, attrs));
                        break;
                    case OgcGeometryType.MultiPolygon:
                        polygons.Add(CreateShapePolygonFromMultiPolygon(item.Geometry, attrs));
                        break;

                    default:
                        break;
                }
            }
            return polygons;
        }

        private static ShapePolygon CreateShapePolygonFromSimplePolygon(Geometry polygonGeometry, Dictionary<string, object> attributes)
        {
            List<SDG.Point> points = new();
            points.AddRange(polygonGeometry.Coordinates.Select(coordinate => new SDG.Point(coordinate.X, coordinate.Y)));

            return new ShapePolygon(new SDG.Polygon(points)) { Attributes = attributes };
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
