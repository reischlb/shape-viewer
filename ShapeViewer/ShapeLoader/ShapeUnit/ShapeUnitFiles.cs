using ShapeLoader.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLoader.ShapeUnit
{
    public class ShapeUnitFiles
    {
        public string Name =>
            DBFFile is not null ? Path.GetFileNameWithoutExtension(DBFFile) :
            SHXFile is not null ? Path.GetFileNameWithoutExtension(SHXFile) :
            SHPFile is not null ? Path.GetFileNameWithoutExtension(SHPFile) :
            "Invalid shape unit";

        public override string ToString() => Name;

        public string? DBFFile { get; init; }
        // public string? PRJFile { get; init; }
        public string? SHPFile { get; init; }
        public string? SHXFile { get; init; }

        public void LoadSHP()
        {
            if (SHPFile is not null)
                SHPLoader.LoadSHP(new FileInfo(SHPFile));
        }
        public void LoadSHX()
        {
            if (SHXFile is not null)
                SHXLoader.LoadSHX(new FileInfo(SHXFile));
        }
        public void LoadDBF()
        {
            if (DBFFile is not null)
                DBFLoader.LoadDBF(new FileInfo(DBFFile));
        }

        public void LoadUnit()
        {
            LoadDBF();
            LoadSHP();
            LoadSHX();
        }
    }
}
