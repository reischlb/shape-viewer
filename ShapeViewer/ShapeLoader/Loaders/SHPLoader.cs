using DotSpatial.Data;

namespace ShapeLoader.Loaders
{
    public static class SHPLoader
    {
        public static void LoadSHP(FileInfo shpFile)
        {
            var shapefile = Shapefile.Open(shpFile.FullName);
        }
    }
}
