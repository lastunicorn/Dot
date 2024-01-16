// Dot
// Copyright (C) 2020-2024 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using DustInTheWind.Dot.GameSavesAccess;
using DustInTheWind.Dot.Ports.GameSavesAccess;
using Xunit;

namespace Dot.Tests.Domain.SaveModel;

public class GameSlotRepositoryTests
{
    [Fact]
    public void Test1()
    {
        GameSlotRepository gameSlotRepository = new();

        StorageData storageData = new()
        {
            SaveTime = new DateTime(2000, 06, 13),
            Version = new Version(1, 2, 3, 4)
        };

        storageData.Add("key1", "value1");
        storageData.Add("key2", "value2");

        GameSlot gameSlot = new()
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