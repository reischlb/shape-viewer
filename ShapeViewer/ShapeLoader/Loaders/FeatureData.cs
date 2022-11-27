using DotSpatial.Data;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;
using System.Data;

namespace ShapeLoader.Loaders
{
    public class FeatureData
    {
        private IFeatureSet Owner { get; init; }
        private int IndexInOwner { get; init; }

        private Geometry _geometry;
        public Geometry Geometry
        {
            get
            {
                return _geometry;
            }
            private set
            {
                _geometry = value;
                Owner.Features[IndexInOwner].Geometry = Geometry;
            }
        }

        public DataRow Data { get; init; }

        public DataColumnCollection Header => Data.Table.Columns;

        public OgcGeometryType GeometryType => Geometry.OgcGeometryType;

        public void DeleteCoordinate(int index)
        {
            var asList = Geometry.Coordinates.ToList();
            asList.RemoveAt(index);

            UpdateGeometryWithCoordinates(asList);
        }

        public void InsertCoordinate(Coordinate coordinate, int index)
        {
            var asList = Geometry.Coordinates.ToList();
            asList.Insert(index, coordinate);

            UpdateGeometryWithCoordinates(asList);
        }

        public void ChangeCoordinate(Coordinate coordinate, int index)
        {
            var asList = Geometry.Coordinates.ToList();
            asList[index] = coordinate;

            UpdateGeometryWithCoordinates(asList);
        }

        private void UpdateGeometryWithCoordinates(List<Coordinate> asList)
        {
            var factory = Geometry.Factory;
            CoordinateSequence cordSeq = factory.CoordinateSequenceFactory.Create(asList.ToArray());
            UpdateGeometryWithCoordinates(factory, cordSeq);
        }

        private void UpdateGeometryWithCoordinates(GeometryFactory factory, CoordinateSequence cordSeq)
        {
            Geometry = Geometry switch
            {
                Polygon => factory.CreatePolygon(cordSeq),
                LinearRing => factory.CreateLinearRing(cordSeq),
                LineString => factory.CreateLineString(cordSeq),
                MultiPoint => factory.CreateMultiPoint(cordSeq),
                Point => factory.CreatePoint(cordSeq),
                _ => factory.CreateEmpty(Dimension.Unknown),
            };
        }

        public void SaveAs(string fileName, bool overwrite = true)
        {
            Owner.SaveAs(fileName, overwrite);
        }

        public void Save()
        {
            Owner.Save();
        }

        public FeatureData(Geometry geometry, DataRow data, IFeatureSet owner, int index)
        {
            _geometry = geometry;
            Data = data;
            Owner = owner;
            IndexInOwner = index;
        }
    }
}
