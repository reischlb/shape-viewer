using DotSpatial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLoader.Loaders
{
    internal class SHXLoader
    {
        public static void LoadSHX(FileInfo shxFile)
        {
            using var shpStream = File.OpenRead(shxFile.FullName);

            ShapefileIndexFile indexFile = new() { Filename = shxFile.FullName};
            indexFile.Open(shxFile.FullName);

            var header = indexFile.Header;
            var shapes = indexFile.Shapes;



        }
    }
}
