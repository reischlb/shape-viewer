using DotSpatial.Data;
using NetTopologySuite.Geometries;
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

        public static List<FeatureData> GetObjects(this IFeatureSet featureSet)
        {
            List<FeatureData> result = new();

            for (int i = 0; i < featureSet.Features.Count; i++)
            {
                FeatureData data = new(
                    geometry: featureSet.Features[i].Geometry,
                    data: featureSet.DataTable.Rows[i],
                    owner: featureSet,
                    index: i);
                result.Add(data);
            }
            return result;
        }
    }
}
