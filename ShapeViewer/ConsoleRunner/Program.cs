// See https://aka.ms/new-console-template for more information
using ShapeLoader.Loaders;
using System.IO;


//SHPLoader.LoadSHP(new FileInfo(@"../../../../../examples/gis_osm_places_free_1.cpg"));

FolderLoader.LoadFilesInFolder(new DirectoryInfo(@"../../../../../examples"));
//FolderLoader.LoadFilesInFolder(new DirectoryInfo(@"C:\Users\reisc\OneDrive\Asztali gép\mygeodata"));
//FolderLoader.LoadFilesInFolder(new DirectoryInfo(@"C:\Users\reisc\OneDrive\Asztali gép\ne_10m_admin_0_countries_deu"));