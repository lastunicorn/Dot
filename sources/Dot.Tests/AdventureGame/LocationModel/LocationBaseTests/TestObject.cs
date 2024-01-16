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
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace Dot.Tests.AdventureGame.LocationModel.LocationBaseTests;

internal class TestObject : ObjectBase
{
    public override string Id { get; } = "test-object";

    public override string Name { get; } = "test object";

    public override IEnumerable LookAt()
    {
        throw new NotImplementedException();
    }
}