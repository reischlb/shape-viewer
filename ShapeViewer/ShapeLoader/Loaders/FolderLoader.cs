namespace ShapeLoader.Loaders
{
    public class FolderLoader
    {
        public static void LoadFilesInFolder(DirectoryInfo folder)
        {
            var shapeFiles = folder.GetFiles().Where(file => file.Extension == ".shp");


            foreach (var file in shapeFiles)
            {
                SHPLoader.LoadSHP(file);
            }
        }
    }
}
