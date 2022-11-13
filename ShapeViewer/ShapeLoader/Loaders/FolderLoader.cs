using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLoader.Loaders
{
    public class FolderLoader
    {
        public static void LoadFilesInFolder (DirectoryInfo folder)
        {
            foreach (var file in folder.GetFiles())
            {
                switch (file.Extension)
                {
                    case ".shx": SHXLoader.LoadSHX(file); break;
                    case ".shp": SHPLoader.LoadSHP(file); break;
                    case ".dbf": DBFLoader.LoadDBF(file); break;
                }
            }
        }
    }
}
