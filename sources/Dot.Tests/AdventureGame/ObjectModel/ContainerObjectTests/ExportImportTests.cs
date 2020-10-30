using System.Linq;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.SaveModel;
using Xunit;

namespace Dot.Tests.AdventureGame.ObjectModel.ContainerObjectTests
{
    public class ExportImportTests
    {
        [Fact]
        public void HavingAContainerObject_WhenImportingFromStorageNodeContainingOneChild_ThenOneChildIsCreated()
        {
            TestContainerObject containerObject1 = new TestContainerObject();
            TestObject obj = new TestObject();
            containerObject1.AddObject(obj);

            StorageNode storageNode = containerObject1.Export();

            TestContainerObject containerObject2 = new TestContainerObject();
            containerObject2.Import(storageNode);

            Assert.Equal(1, containerObject2.Count());

            IObject actualObject = containerObject2.First();
            Assert.Equal(obj.Id, actualObject.Id);
            Assert.Equal(obj.Name, actualObject.Name);
        }
    }
}