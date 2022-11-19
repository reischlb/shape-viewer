using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLoader.Loaders
{
    public class FeatureData
    {
        public Geometry Geometry { get; init; }

        public DataRow Data { get; init; }

        public DataColumnCollection Header => Data.Table.Columns;

        public OgcGeometryType GeometryType => Geometry.OgcGeometryType;

        public FeatureData(Geometry geometry, DataRow data)
        {
            Geometry = geometry;
            Data = data;
        }
    }
}
