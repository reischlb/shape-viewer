using DotSpatial.Data;
using NetTopologySuite.Geometries;
using ShapeData.Data;
using System.Data;

namespace ShapeLoader.Loaders
{
    public static class FeatureSetExtensions
    {
        public static List<Geometry> GetGeometries(this IFeatureSet featureSet)
        {
            return featureSet.Features.Select(feature => feature.Geometry).ToList();
        }

        public static List<object[]> GetAttributes(this IFeatureSet featureSet)
        {
            return (from DataRow row in featureSet.DataTable.Rows
                    select row.ItemArray).ToList();
        }

        public static List<string> GetAttributeHeaders(this IFeatureSet featureSet)
        {
            return (from DataColumn column in featureSet.DataTable.Columns
                    select column.ColumnName).ToList();
        }

        public static List<(Geometry Geometry, object[] Attributes)> GetZippedAttributes(this IFeatureSet featureSet)
        {
            var geometries = featureSet.GetGeometries();
            var attributes = featureSet.GetAttributes();

            return geometries.Zip(attributes).ToList();
        }
    }
}
