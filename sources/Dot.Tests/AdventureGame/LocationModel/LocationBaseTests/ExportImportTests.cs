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

namespace Dot.Tests.AdventureGame.LocationModel.LocationBaseTests;

public class ExportImportTests
{
    [Fact]
    public void WhenExportImport_ChildrenAreRestored()
    {
        TestLocation testLocation1 = new();
        testLocation1.InitializeNew();
        ExportNode storageNode = testLocation1.Export();

        TestLocation testLocation2 = new();
        testLocation2.Import(storageNode);

        Assert.Equal(testLocation1.Children.Count, testLocation2.Children.Count);
        Assert.Equal(typeof(TestObject), testLocation2.Children.First().GetType());
    }

    [Fact]
    public void WhenExportImport_AddOnsAreRestored()
    {
        TestLocation testLocation1 = new();
        testLocation1.InitializeNew();
        ExportNode storageNode = testLocation1.Export();

        TestLocation testLocation2 = new();
        testLocation2.Import(storageNode);

        Assert.Equal(1, testLocation2.AddOns.Count);
        Assert.Equal(typeof(TestAddOn), testLocation2.AddOns.First().GetType());
    }
}