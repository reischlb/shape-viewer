using ShapeLoader.FolderHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLoader.Loaders
{
    public class FolderLoader
    {
        public static void LoadFilesInFolder(DirectoryInfo folder)
        {
            var units = new FolderExplorer(folder).CollectShapeUnits();

            foreach (var unit in units)
            {
                unit.LoadUnit();
            }
        }
    }
}
