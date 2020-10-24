using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.SaveModel;
using DustInTheWind.Dot.GameStorage;
using DustInTheWind.Dot.GameStorage.Binary;
using Xunit;

namespace Dot.Tests
{
    public class GameSlotRepositoryTests
    {
        [Fact]
        public void Test1()
        {
            GameSlotRepository gameSlotRepository = new GameSlotRepository();

            StorageData storageData = new StorageData
            {
                SaveTime = new DateTime(2000, 06, 13),
                Version = new Version(1, 2, 3, 4),
            };

            storageData.Add("key1", "value1");
            storageData.Add("key2", "value2");

            GameSlot gameSlot = new GameSlot
            {
                Id = 12,
                Data = storageData
            };
            gameSlotRepository.AddOrReplace(gameSlot);

            List<GameSlot> gameSlots = gameSlotRepository.GetAll().ToList();

            Assert.Equal(1, gameSlots.Count);

            Assert.Equal(12, gameSlots[0].Id);
            Assert.Equal(2, gameSlots[0].Data.Count);

            Assert.Equal("value1", gameSlots[0].Data["key1"]);
            Assert.Equal("value2", gameSlots[0].Data["key2"]);

            Assert.Equal(new DateTime(2000, 06, 13), gameSlots[0].Data.SaveTime);
            Assert.Equal(new Version(1, 2, 3, 4), gameSlots[0].Data.Version);
        }
    }
}