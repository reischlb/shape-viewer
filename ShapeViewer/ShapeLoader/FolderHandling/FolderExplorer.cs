using ShapeLoader.ShapeUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLoader.FolderHandling
{
    public class FolderExplorer
    {
        public DirectoryInfo Folder { get; init; }
        private List<string> FilesToExplore { get; }

        public FolderExplorer(DirectoryInfo folder)
        {
            Folder = folder;
            FilesToExplore = Folder.GetFiles().Select(f => f.FullName).ToList();
        }

        public IReadOnlyList<ShapeUnitFiles> CollectShapeUnits()
        {
            List<ShapeUnitFiles> shapeUnits = new();

            var files = FilesToExplore.Select(str => new FileInfo(str)).ToList();

            while (files.Count > 0)
            {
                var shapeUnit = CollectShapeUnitFiles(files[0]);
                RemoveFoundFilesFromFilesList(shapeUnit);
                if (shapeUnit is not null)
                    shapeUnits.Add(shapeUnit);
                files = FilesToExplore.Select(str => new FileInfo(str)).ToList();
            }

            return shapeUnits;
        }

        private static ShapeFileExtension GetExtension(FileInfo file) => file.Extension switch
        {
            //".cpg" => ShapeFileExtension.CPG,
            ".shx" => ShapeFileExtension.SHX,
            ".shp" => ShapeFileExtension.SHP,
            //".prj" => ShapeFileExtension.PRJ,
            ".dbf" => ShapeFileExtension.DBF,
            _ => ShapeFileExtension.NotSupported,
        };

        private ShapeUnitFiles? CollectShapeUnitFiles(FileInfo file)
        {
            if (GetExtension(file) is ShapeFileExtension.NotSupported)
            {
                FilesToExplore.Remove(file.FullName);
                return null;
            }
            var CPGFullName = Path.ChangeExtension(file.FullName, "cpg");
            var SHPFullName = Path.ChangeExtension(file.FullName, "shp");
            var SHXFullName = Path.ChangeExtension(file.FullName, "shx");
            var DBFFullName = Path.ChangeExtension(file.FullName, "dbf");
            var PRJFullName = Path.ChangeExtension(file.FullName, "prj");
            return new ShapeUnitFiles()
            {
                //CPGFile = FilesToExplore.Contains(CPGFullName) ? CPGFullName : null,
                SHPFile = FilesToExplore.Contains(SHPFullName) ? SHPFullName : null,
                SHXFile = FilesToExplore.Contains(SHXFullName) ? SHXFullName : null,
                DBFFile = FilesToExplore.Contains(DBFFullName) ? DBFFullName : null,
                //PRJFile = FilesToExplore.Contains(PRJFullName) ? PRJFullName : null,
            };
        }

        private void RemoveFoundFilesFromFilesList(ShapeUnitFiles? foundFiles)
        {
          //  if (foundFiles?.CPGFile is not null)
          //      FilesToExplore.Remove(foundFiles.CPGFile);
            if (foundFiles?.SHXFile is not null)
                FilesToExplore.Remove(foundFiles.SHXFile);
            if (foundFiles?.SHPFile is not null)
                FilesToExplore.Remove(foundFiles.SHPFile);
         //   if (foundFiles?.PRJFile is not null)
         //       FilesToExplore.Remove(foundFiles.PRJFile);
            if (foundFiles?.DBFFile is not null)
                FilesToExplore.Remove(foundFiles.DBFFile);
        }
    }
}
