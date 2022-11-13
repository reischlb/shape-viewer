using DotSpatial.Data;

namespace ShapeLoader.Loaders
{
    public static class DBFLoader
    {
       public static void LoadDBF(FileInfo dbfFile)
        {

            AttributeTable attrTable = new AttributeTable();

            attrTable.Open(dbfFile.FullName);
        
            
        }
    }
}
