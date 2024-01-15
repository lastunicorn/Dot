﻿using System.Linq;
using DustInTheWind.Dot.AdventureGame.ExportModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Ports.GameSavesAccess;
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

            ExportNode storageNode = containerObject1.Export();

            TestContainerObject containerObject2 = new TestContainerObject();
            containerObject2.Import(storageNode);

            Assert.Equal(1, containerObject2.Count());

            IObject actualObject = containerObject2.First();
            Assert.Equal(obj.Id, actualObject.Id);
            Assert.Equal(obj.Name, actualObject.Name);
        }
    }
}