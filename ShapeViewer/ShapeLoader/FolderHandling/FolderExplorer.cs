using ShapeLoader.ShapeUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLoader.FolderHandling
{
    internal class FolderExplorer
    {
        public DirectoryInfo Folder { get; init; }
        private List<string> Files { get; }

        public FolderExplorer(DirectoryInfo folder)
        {
            Folder = folder;
            Files = Folder.GetFiles().Select(f => f.FullName).ToList();
        }

        public IReadOnlyList<ShapeUnitFiles> CollectShapeUnits()
        {
            List<ShapeUnitFiles> shapeUnits = new();

            var files = Folder.GetFiles().ToList();

            while (files.Count > 0)
            {
                var shapeUnit = CollectShapeUnitFiles(files[0]);
                RemoveFoundFilesFromFilesList(shapeUnit);
                shapeUnits.Add(shapeUnit);
            }

            return shapeUnits;
        }

        private static ShapeFileExtension GetExtension(FileInfo file) => file.Extension switch
        {
            ".cpg" => ShapeFileExtension.CPG,
            ".shx" => ShapeFileExtension.SHX,
            ".shp" => ShapeFileExtension.SHP,
            ".prj" => ShapeFileExtension.PRJ,
            ".dbf" => ShapeFileExtension.DBF,
            _ => ShapeFileExtension.NotSupported,
        };

        private ShapeUnitFiles CollectShapeUnitFiles(FileInfo file)
        {
            string filenameWithoutExtension = Path.GetFileNameWithoutExtension(file.FullName);
            return new ShapeUnitFiles()
            {
                CPGFile = Files.Contains($"{filenameWithoutExtension}.cpg") ? $"{filenameWithoutExtension}.cpg" : null,
                SHPFile = Files.Contains($"{filenameWithoutExtension}.shp") ? $"{filenameWithoutExtension}.shp" : null,
                SHXFile = Files.Contains($"{filenameWithoutExtension}.shx") ? $"{filenameWithoutExtension}.shx" : null,
                DBFFile = Files.Contains($"{filenameWithoutExtension}.dbf") ? $"{filenameWithoutExtension}.dbf" : null,
                PRJFile = Files.Contains($"{filenameWithoutExtension}.prj") ? $"{filenameWithoutExtension}.prj" : null,
            };
        }

        private void RemoveFoundFilesFromFilesList(ShapeUnitFiles foundFiles)
        {
            if (foundFiles.CPGFile is not null)
                Files.Remove(foundFiles.CPGFile);
            if (foundFiles.SHXFile is not null)
                Files.Remove(foundFiles.SHXFile);
            if (foundFiles.SHPFile is not null)
                Files.Remove(foundFiles.SHPFile);
            if (foundFiles.PRJFile is not null)
                Files.Remove(foundFiles.PRJFile);
            if (foundFiles.DBFFile is not null)
                Files.Remove(foundFiles.DBFFile);
        }
    }
}
