using GeoAPI.IO;
using MapControl;
using ShapeLoader.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viewer
{
    public class PointItem
    {
        public string Name { get; set; }

        public Location Location { get; set; }
    }

    public class PolylineItem
    {
        public LocationCollection Locations { get; set; }
    }

    public class MapViewModel
    {
        public List<PointItem> Points { get; } = new List<PointItem>();
        public List<PointItem> Pushpins { get; } = new List<PointItem>();
        public List<PolylineItem> Polylines { get; } = new List<PolylineItem>();

        public MapViewModel()
        {
            DirectoryInfo info = new DirectoryInfo("examples");
            List<FeatureData> data = ShapeLoader.Loaders.FolderLoader.LoadFilesInFolder(info);
            int i = 0;
            foreach (var item in data){ 
                if (item.GeometryType.ToString() == "Polygon" || item.GeometryType.ToString() == "MultiPolygon")
                {
                    i += 1;
                    if (i > 1000)
                        return;
                    PolylineItem polylineItem = new PolylineItem();
                    polylineItem.Locations = LocationCollection.Parse("");

                                 
                    
                    foreach(var item2 in item.Geometry.Coordinates)
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

            
        }
    }
}
