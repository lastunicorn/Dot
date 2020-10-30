using DustInTheWind.Dot.Domain.SaveModel;
using Xunit;

namespace Dot.Tests.AdventureGame.ObjectModel.ObjectBaseTests
{
    public class ExportImportTests
    {
        [Fact]
        public void HavingAnObjectBase_WhenImportingFromStorageNodeContainingIsVisibleTrue_ThenIsVisibleIsSetToTrue()
        {
            TestObject objectBase1 = new TestObject();
            objectBase1.IsVisible = true;

            StorageNode storageNode = objectBase1.Export();

            TestObject objectBase2 = new TestObject();
            objectBase2.Import(storageNode);

            Assert.True(objectBase2.IsVisible);
        }

        [Fact]
        public void HavingAnObjectBase_WhenImportingFromStorageNodeContainingIsVisibleFalse_ThenIsVisibleIsSetToFalse()
        {
            TestObject objectBase1 = new TestObject();
            objectBase1.IsVisible = false;

            StorageNode storageNode = objectBase1.Export();

            TestObject objectBase2 = new TestObject();
            objectBase2.Import(storageNode);

            Assert.False(objectBase2.IsVisible);
        }
    }
}