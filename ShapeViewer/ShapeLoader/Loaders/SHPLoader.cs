using DotSpatial.Data;

namespace ShapeLoader.Loaders
{
    public static class SHPLoader
    {
        public static void LoadSHP(FileInfo shpFile)
        {
            using var shpStream = File.OpenRead(shpFile.FullName);

            Shapefile shpPoint = new PointShapefile(shpFile.FullName);
            Shapefile shpMultiPoint = new MultiPointShapefile(shpFile.FullName);
            Shapefile shpLine = new LineShapefile(shpFile.FullName);
            Shapefile shpPolygon = new PolygonShapefile(shpFile.FullName);


        }
    }
}
