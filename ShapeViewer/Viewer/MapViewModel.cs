using GeoAPI.Geometries;
using GeoAPI.IO;
using MapControl;
using ShapeLoader.Loaders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Viewer
{
    public  class PointItem : INotifyPropertyChanged
    {
        public string Name { get; set; }
        private SolidColorBrush _color;
        public FeatureData featureData { get; set; }
        public SolidColorBrush color
        {
            get
            {
                return _color;
            }

            set
            {

                _color = value;
                NotifyPropertyChanged();
            }
        }

        private MapControl.Location _location;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MapControl.Location Location
        {
            get
            {
                return _location;
            }
            set
            {

                _location = value;
                NotifyPropertyChanged();
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

    public class PolylineItem : INotifyPropertyChanged
    {
        public LocationCollection Locations { get; set; }
        public FeatureData featureData { get; set; }

        private SolidColorBrush _color;
        public   SolidColorBrush color
        {
            get
            {
                return _color;
            }

            set
            {

                _color = value;
                NotifyPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

    public class MapViewModel
    {
        public static ObservableCollection<PointItem> Points { get; } = new ObservableCollection<PointItem>();
        public List<PointItem> Pushpins { get; } = new List<PointItem>();
        public static ObservableCollection<PolylineItem> Polylines { get; } = new ObservableCollection<PolylineItem>();
        

        public MapViewModel()
        {
            DirectoryInfo info = new DirectoryInfo("examples");
            List<FeatureData> data = ShapeLoader.Loaders.FolderLoader.LoadFilesInFolder(info);
            int i = 0;
            int limit = 30;
            PointItem pointItem = new PointItem();
            pointItem.Location = new MapControl.Location(50, 50);
            pointItem.color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 0));
            Points.Add(pointItem);
            foreach (var item in data){ 
                if (item.GeometryType.CompareTo(NetTopologySuite.Geometries.OgcGeometryType.Polygon)==0 || item.GeometryType.CompareTo(NetTopologySuite.Geometries.OgcGeometryType.MultiPolygon)==0)
                {
                    i += 1;
                    if (i > limit)
                        return;
                    PolylineItem polylineItem = new PolylineItem();
                    polylineItem.Locations = LocationCollection.Parse("");
                    polylineItem.featureData = item;
                    polylineItem.color=new SolidColorBrush(System.Windows.Media.Color.FromArgb(255,255,0,0));





                    foreach (var item2 in item.Geometry.Coordinates)
                    {
                        polylineItem.Locations.Add(Math.Abs(item2.CoordinateValue.X), Math.Abs(item2.CoordinateValue.Y*-1));
                    }

                    Polylines.Add(polylineItem);
                }
                else if (item.GeometryType==NetTopologySuite.Geometries.OgcGeometryType.Point)
                {
                    i += 1;
                    if (i > limit)
                        return;

                   /* PointItem pointItem = new PointItem();
                    pointItem.featureData=item;
                    pointItem.Location.Latitude = item.Geometry.Coordinate.X;
                    pointItem.Location.Longitude = item.Geometry.Coordinate.Y;
                    pointItem.color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 0));
                    Points.Add(pointItem);*/



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
