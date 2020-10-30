using System;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.Domain.SaveModel;

namespace Dot.Tests.AdventureGame.LocationModel.LocationBaseTests
{
    internal class TestAddOn : IAddOn
    {
        public string Id { get; } = "test-addon";

        public GameBase Game { get; set; }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public StorageNode Export()
        {
            return new StorageNode
            {
                ObjectType = GetType()
            };
        }

        public void Import(StorageNode storageNode)
        {
        }
    }
}