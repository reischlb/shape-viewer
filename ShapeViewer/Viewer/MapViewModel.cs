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
        public FeatureData featureData { get; set; }
    }

    public class MapViewModel
    {
        public List<PointItem> Points { get; } = new List<PointItem>();
        public List<PointItem> Pushpins { get; } = new List<PointItem>();
        public static ObservableCollection<PolylineItem> Polylines { get; } = new ObservableCollection<PolylineItem>();

        public MapViewModel()
        {
            DirectoryInfo info = new DirectoryInfo("examples");
            List<FeatureData> data = ShapeLoader.Loaders.FolderLoader.LoadFilesInFolder(info);
            int i = 0;
            foreach (var item in data){ 
                if (item.GeometryType.CompareTo(NetTopologySuite.Geometries.OgcGeometryType.Polygon)==0 || item.GeometryType.CompareTo(NetTopologySuite.Geometries.OgcGeometryType.MultiPolygon)==0)
                {
                    i += 1;
                    if (i > 10)
                        return;
                    PolylineItem polylineItem = new PolylineItem();
                    polylineItem.Locations = LocationCollection.Parse("");
                    polylineItem.featureData = item;





                    foreach (var item2 in item.Geometry.Coordinates)
                    {
                        polylineItem.Locations.Add(item2.CoordinateValue.X*-1, item2.CoordinateValue.Y*-1);
                    }

                    Polylines.Add(polylineItem);
                }
                else
                {
                    int a = 1;
                }


            }

            

           /* LoadSHP();*/
        }

       /* public void LoadSHP()
        {
            var fileData = FolderLoader.LoadFilesInFolder(new DirectoryInfo(@"../../../../../examples"));

            foreach (var data in fileData)
            {
                if (data is null) continue;
                if (data.GeometryType is OgcGeometryType.Point)
                    Points.Add(new PointItem() { Location = new Location(data.Geometry.Coordinates[0].X, data.Geometry.Coordinates[0].Y) });
            }
        }*/
    }
}
