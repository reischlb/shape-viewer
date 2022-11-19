﻿using DotSpatial.Data;
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

        public static List<FeatureData> GetObjects(this IFeatureSet featureSet)
        {
            List<FeatureData> result = new();

            for (int i = 0; i < featureSet.Features.Count; i++)
            {
                FeatureData data = new(
                    geometry: featureSet.Features[i].Geometry,
                    data: featureSet.DataTable.Rows[i]);
                result.Add(data);
            }

            var bol = result.Where(fd => (string)fd.Data[0] == "BOL").Select(x => x).First();

            bol.Data[1] = "hello";

            featureSet.SaveAs(@"C:\Users\reisc\OneDrive\Asztali gép\myshape3.shp", true);
            return result;
        }
    }
}
