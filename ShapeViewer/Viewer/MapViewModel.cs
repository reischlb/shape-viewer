using GeoAPI.Geometries;
using GeoAPI.IO;
using MapControl;
using ShapeLoader.Loaders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viewer
{
    public class PointItem
    {
        public string Name { get; set; }

        public MapControl.Location Location { get; set; }
    }

    public class PolylineItem
    {
        public LocationCollection Locations { get; set; }
        public FeatureData FeatureData { get; set; }
    }

    public class MapViewModel
    {
        public List<PointItem> Points { get; } = new List<PointItem>();
        public List<PointItem> Pushpins { get; } = new List<PointItem>();
        public static ObservableCollection<PolylineItem> Polylines { get; } = new ObservableCollection<PolylineItem>();

        public MapViewModel()
        {
          //  DirectoryInfo info = new DirectoryInfo("examples");
            DirectoryInfo info = new(@"../../../../../examples");
            List<FeatureData> data = FolderLoader.LoadFilesInFolder(info);
            int i = 0;
            foreach (var item in data)
            {
                if (item.GeometryType == NetTopologySuite.Geometries.OgcGeometryType.Polygon || item.GeometryType == NetTopologySuite.Geometries.OgcGeometryType.MultiPolygon)
                {
                    i += 1;
                    if (i > 10)
                        return;
                    PolylineItem polylineItem = new PolylineItem();
                    polylineItem.Locations = LocationCollection.Parse("");
                    polylineItem.FeatureData = item;

                    foreach (var item2 in item.Geometry.Coordinates)
                    {
                        polylineItem.Locations.Add(item2.CoordinateValue.X * -1, item2.CoordinateValue.Y * -1);
                    }

                    Polylines.Add(polylineItem);
                }
            }
        }
    }
}
