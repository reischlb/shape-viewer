using MapControl;
using MapControl.Caching;
using MapControl.UiTools;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Viewer
{
    public partial class MainWindow : Window
    {
        private PolylineItem selectedPolyLineItem;
        private int selectedPolyLineIndex=-1;
        private PointItem selectedPointItem;
        private int selectedPointIndex = -1;

        static MainWindow()
        {
            ImageLoader.HttpClient.DefaultRequestHeaders.Add("User-Agent", "XAML Map Control Test Application");

            TileImageLoader.Cache = new ImageFileCache(TileImageLoader.DefaultCacheFolder);

            var bingMapsApiKeyPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MapControl", "BingMapsApiKey.txt");

            if (File.Exists(bingMapsApiKeyPath))
            {
                BingMapsTileLayer.ApiKey = File.ReadAllText(bingMapsApiKeyPath)?.Trim();
            }
        }

        public MainWindow()
        {

            InitializeComponent();

            AddChartServerLayer();

            if (TileImageLoader.Cache is ImageFileCache cache)
            {
                Loaded += async (s, e) =>
                {
                    await Task.Delay(2000);
                    await cache.Clean();
                };
            }
        }

        partial void AddChartServerLayer();

        private void ResetHeadingButtonClick(object sender, RoutedEventArgs e)
        {
            map.TargetHeading = 0d;
        }

        private void MapMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                polygonText.Text = "";
                SolidColorBrush defaultColor = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                SolidColorBrush selectColor= new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                foreach (var item in MapViewModel.Polylines)
                {
                    item.color= defaultColor;
                }
                foreach (var item in MapViewModel.Points)
                {
                    item.color = defaultColor;
                }

                map.TargetCenter = map.ViewToLocation(e.GetPosition(map));
                MapControl.Location cursor= map.ViewToLocation(e.GetPosition(map));
                double minDistanceForPoly = 10;
                double minDistanceForPoint = 10;
                PolylineItem minPolyLine = new PolylineItem();
                int idForPoly = -1;
                int indexForPoly = 0;
                int indexForPoint = 0;

                PointItem minPoint = new PointItem();
                foreach (PolylineItem polyItem in MapViewModel.Polylines)
                {
                    double xCoord = polyItem.Locations.Average(x => x.Latitude);
                    double yCoord = polyItem.Locations.Average(x => x.Longitude);
                    var distance = Math.Sqrt((Math.Pow(cursor.Latitude - xCoord, 2) + Math.Pow(cursor.Longitude - yCoord, 2)));
                    if(distance < minDistanceForPoly)
                    {
                        minDistanceForPoly = distance;
                        minPolyLine = polyItem;
                        selectedPolyLineIndex= indexForPoly;
                    }

                    indexForPoly++;
                }

                foreach (PointItem pointItem in MapViewModel.Points)
                {
                    double xCoord = pointItem.Location.Latitude;
                    double yCoord = pointItem.Location.Longitude;
                    var distance = Math.Sqrt((Math.Pow(cursor.Latitude - xCoord, 2) + Math.Pow(cursor.Longitude - yCoord, 2)));
                    if (distance < minDistanceForPoint)
                    {
                        minDistanceForPoint = distance;
                        minPoint = pointItem;
                        selectedPointIndex = indexForPoint;
                    }

                    indexForPoint++;
                }

                if (selectedPolyLineIndex == -1 && selectedPointIndex == -1)
                    return;
                if(minDistanceForPoly<minDistanceForPoint)
                {
                    selectedPointIndex = -1;
                    foreach (var item in minPolyLine.Locations)
                        polygonText.Text += item.ToString() + "\r\n";

                    geometryIdValue.Content = selectedPolyLineIndex.ToString();
                    geometryAreaValue.Content = minPolyLine.featureData.Geometry.Area.ToString();
                    geometryPointsValue.Content = minPolyLine.featureData.Geometry.Coordinates.Length.ToString();
                    geometryTypeValue.Content = minPolyLine.featureData.GeometryType.ToString();
                    selectedPolyLineItem = minPolyLine;
                    minPolyLine.color = selectColor;

                    MapViewModel.Polylines[selectedPolyLineIndex] = minPolyLine;
                }
                else
                {
                    selectedPolyLineIndex = -1;
                    polygonText.Text = minPoint.Location.Latitude.ToString() + " " + minPoint.Location.Longitude.ToString();
                    geometryIdValue.Content = selectedPointIndex.ToString();
                    geometryAreaValue.Content = 0;
                    geometryPointsValue.Content = "1";
                    geometryTypeValue.Content = "Point";
                    selectedPointItem = minPoint;
                    minPoint.color = selectColor;

                    MapViewModel.Points[selectedPointIndex] = minPoint;
                }
                



                int a = 1;

            }
        }

        private void MapMouseMove(object sender, MouseEventArgs e)
        {
            var location = map.ViewToLocation(e.GetPosition(map));

            if (location != null)
            {
                var latitude = (int)Math.Round(location.Latitude * 60000d);
                var longitude = (int)Math.Round(MapControl.Location.NormalizeLongitude(location.Longitude) * 60000d);
                var latHemisphere = 'N';
                var lonHemisphere = 'E';

                if (latitude < 0)
                {
                    latitude = -latitude;
                    latHemisphere = 'S';
                }

                if (longitude < 0)
                {
                    longitude = -longitude;
                    lonHemisphere = 'W';
                }

                mouseLocation.Text = string.Format(CultureInfo.InvariantCulture,
                    "{0}  {1:00} {2:00.000}\n{3} {4:000} {5:00.000}",
                    latHemisphere, latitude / 60000, (latitude % 60000) / 1000d,
                    lonHemisphere, longitude / 60000, (longitude % 60000) / 1000d);
            }
            else
            {
                mouseLocation.Text = string.Empty;
            }
        }

        private void MapMouseLeave(object sender, MouseEventArgs e)
        {
            mouseLocation.Text = string.Empty;
        }

        private void MapManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior.DesiredDeceleration = 0.001;
        }

        private void MapItemTouchDown(object sender, TouchEventArgs e)
        {
            var mapItem = (MapItem)sender;
            mapItem.IsSelected = !mapItem.IsSelected;
            e.Handled = true;
        }


        private void Save(object sender, RoutedEventArgs e)
        {
            if( selectedPolyLineIndex>-1)
            {
                PolylineItem polylineItem = new PolylineItem();
                polylineItem.color = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                String a = polygonText.Text.Replace("\r\n", " ");
                polylineItem.Locations = LocationCollection.Parse(a);
                MapViewModel.Polylines[selectedPolyLineIndex] = polylineItem;
            }
            else if(selectedPointIndex>-1)
            {
                PointItem pointItem = new PointItem();
                pointItem.color = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                String[] a = polygonText.Text.Split(" ");
                pointItem.Location=new MapControl.Location(Double.Parse(a[0]), Double.Parse(a[1]));
                MapViewModel.Points[selectedPointIndex] = pointItem;

            }
                
            
            int b = 2;
  
        }
    }
}