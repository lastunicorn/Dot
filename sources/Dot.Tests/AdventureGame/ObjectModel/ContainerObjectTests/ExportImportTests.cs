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

using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.GameModel;
using Xunit;

namespace Dot.Tests.AdventureGame.ObjectModel.ContainerObjectTests;

public class ExportImportTests
{
    [Fact]
    public void HavingAContainerObject_WhenImportingFromStorageNodeContainingOneChild_ThenOneChildIsCreated()
    {
        TestContainerObject containerObject1 = new();
        TestObject obj = new();
        containerObject1.AddObject(obj);

        ExportNode storageNode = containerObject1.Export();

        TestContainerObject containerObject2 = new();
        containerObject2.Import(storageNode);

        Assert.Equal(1, containerObject2.Count());

        IObject actualObject = containerObject2.First();
        Assert.Equal(obj.Id, actualObject.Id);
        Assert.Equal(obj.Name, actualObject.Name);
    }
}