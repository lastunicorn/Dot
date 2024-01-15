using System;
using DustInTheWind.Dot.AdventureGame.ExportModel;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.Ports.GameSavesAccess;

namespace Dot.Tests.AdventureGame.LocationModel.LocationBaseTests
{
    internal class TestAddOn : IAddOn
    {
        public string Id { get; } = "test-addon";

        public Game Game { get; set; }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public ExportNode Export()
        {
            return new ExportNode
            {
                ObjectType = GetType()
            };
        }

        public void Import(ExportNode storageNode)
        {
        }
    }
}