using DotSpatial.Data;

namespace ShapeLoader.Loaders
{
    public static class SHPLoader
    {
        public static List<FeatureData> LoadSHP(FileInfo shpFile) =>
            FeatureSet.Open(shpFile.FullName).GetObjects();
    }
}
