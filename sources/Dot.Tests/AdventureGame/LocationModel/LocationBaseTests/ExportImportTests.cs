using System.Linq;
using DustInTheWind.Dot.Ports.GameSavesAccess;
using Xunit;

namespace Dot.Tests.AdventureGame.LocationModel.LocationBaseTests
{
    public class ExportImportTests
    {
        [Fact]
        public void WhenExportImport_ChildrenAreRestored()
        {
            TestLocation testLocation1 = new TestLocation();
            testLocation1.InitializeNew();
            StorageNode storageNode = testLocation1.Export();

            TestLocation testLocation2 = new TestLocation();
            testLocation2.Import(storageNode);

            Assert.Equal(testLocation1.Children.Count, testLocation2.Children.Count);
            Assert.Equal(typeof(TestObject), testLocation2.Children.First().GetType());
        }

        [Fact]
        public void WhenExportImport_AddOnsAreRestored()
        {
            TestLocation testLocation1 = new TestLocation();
            testLocation1.InitializeNew();
            StorageNode storageNode = testLocation1.Export();

            TestLocation testLocation2 = new TestLocation();
            testLocation2.Import(storageNode);

            Assert.Equal(1, testLocation2.AddOns.Count);
            Assert.Equal(typeof(TestAddOn), testLocation2.AddOns.First().GetType());
        }
    }
}