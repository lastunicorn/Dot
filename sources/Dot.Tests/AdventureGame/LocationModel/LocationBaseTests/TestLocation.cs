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

using System.Collections;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace Dot.Tests.AdventureGame.LocationModel.LocationBaseTests;

internal class TestLocation : LocationBase
{
    public override string Id { get; } = "test-location";

    public override string Name { get; } = "test location";

    public new HashSet<IObject> Children => base.Children;

    public new AddOnCollection AddOns => base.AddOns;

    public override IEnumerable LookAt()
    {
        throw new NotImplementedException();
    }

    public override AudioText ResumeDescription { get; }

    public override void InitializeNew()
    {
        TestObject testObject = new();
        base.Children.Add(testObject);

        TestAddOn testAddOn = new();
        base.AddOns.Add(testAddOn);
    }
}