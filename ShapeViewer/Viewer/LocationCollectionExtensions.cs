using MapControl;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Viewer
{
    public static class LocationCollectionExtensions
    {
        public static List<Coordinate> ToCoordinates(this LocationCollection locationCollection)
        {
            return (from location in locationCollection.ToList()
                    select new Coordinate(location.Latitude, location.Longitude))
                    .ToList();
        }

    }
}
