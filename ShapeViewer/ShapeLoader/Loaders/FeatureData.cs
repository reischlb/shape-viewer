using DotSpatial.Data;
using NetTopologySuite.Geometries;
using System.Data;

namespace ShapeLoader.Loaders
{
    public class FeatureData
    {
        private IFeatureSet Owner { get; init; }

        public Geometry Geometry { get; init; }

        public DataRow Data { get; init; }

        public DataColumnCollection Header => Data.Table.Columns;

        public OgcGeometryType GeometryType => Geometry.OgcGeometryType;

       //public Dictionary<string, object[]> ZippedData() {
       //    var row = Data.ItemArray.ToList();
       //    var headers = Header.
       //    
       //}

        public void SaveAs(string fileName, bool overwrite = true)
        {
            Owner.SaveAs(fileName, overwrite);
        }

        public void Save()
        {
            Owner.Save();
        }

        public FeatureData(Geometry geometry, DataRow data, IFeatureSet owner)
        {
            Geometry = geometry;
            Data = data;
            Owner = owner;
        }
    }
}
