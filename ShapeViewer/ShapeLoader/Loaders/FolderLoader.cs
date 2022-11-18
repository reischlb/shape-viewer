using ShapeData.Data;
using System.Data;

namespace ShapeLoader.Loaders
{
    public class FolderLoader
    {
        public static List<IShapeData> LoadFilesInFolder(DirectoryInfo folder)
        {
            List<IShapeData> result = new();

            var shapeFiles = folder.GetFiles().Where(file => file.Extension == ".shp");


            foreach (var file in shapeFiles)
            {
                result.AddRange(SHPLoader.LoadSHP(file));
            }
            return result;
        }
    }
}
