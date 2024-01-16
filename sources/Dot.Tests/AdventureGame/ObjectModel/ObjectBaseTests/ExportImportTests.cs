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

using DustInTheWind.Dot.Domain.GameModel;
using Xunit;

namespace Dot.Tests.AdventureGame.ObjectModel.ObjectBaseTests;

public class ExportImportTests
{
    [Fact]
    public void HavingAnObjectBase_WhenImportingFromStorageNodeContainingIsVisibleTrue_ThenIsVisibleIsSetToTrue()
    {
        TestObject objectBase1 = new()
        {
            IsVisible = true
        };

        ExportNode storageNode = objectBase1.Export();

        TestObject objectBase2 = new();
        objectBase2.Import(storageNode);

        Assert.True(objectBase2.IsVisible);
    }

    [Fact]
    public void HavingAnObjectBase_WhenImportingFromStorageNodeContainingIsVisibleFalse_ThenIsVisibleIsSetToFalse()
    {
        TestObject objectBase1 = new()
        {
            IsVisible = false
        };

        ExportNode storageNode = objectBase1.Export();

        TestObject objectBase2 = new();
        objectBase2.Import(storageNode);

        Assert.False(objectBase2.IsVisible);
    }
}