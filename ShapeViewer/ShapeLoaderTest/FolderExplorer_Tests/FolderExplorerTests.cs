using ShapeLoader.FolderHandling;

namespace ShapeLoaderTest.FolderExplorer_Tests
{
    public class FolderExplorerTests
    {
        [Fact]
        public void FolderExplorerTest_OneUnit()
        {
            FolderExplorer explorer = new(new DirectoryInfo(@"../../../FolderExplorer_Tests/OneUnit"));

            var units = explorer.CollectShapeUnits();

            Assert.Equal(1, units.Count);
        }

        [Fact]
        public void FolderExplorerTest_MoreUnits()
        {
            FolderExplorer explorer = new(new DirectoryInfo(@"../../../FolderExplorer_Tests/MoreUnits"));

            var units = explorer.CollectShapeUnits();

            Assert.Equal(2, units.Count);
        }

        [Fact]
        public void FolderExplorerTest_OneFile()
        {
            FolderExplorer explorer = new(new DirectoryInfo(@"../../../FolderExplorer_Tests/OneFile"));

            var units = explorer.CollectShapeUnits();

            Assert.Equal(1, units.Count);
        }

        [Fact]
        public void FolderExplorerTest_NotShapeExtension()
        {
            FolderExplorer explorer = new(new DirectoryInfo(@"../../../FolderExplorer_Tests/NotShapeExtension"));

            var units = explorer.CollectShapeUnits();

            Assert.Equal(1, units.Count);
        }
    }
}
