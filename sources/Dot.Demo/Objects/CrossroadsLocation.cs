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
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Demo.Objects;

internal class CrossroadsLocation : LocationBase
{
    public override string Id { get; } = "crossroads";

    public override string Name { get; } = "Crossroads";

    public override void InitializeNew()
    {
    }

    public override IEnumerable LookAt()
    {
        yield return CreateDescriptionStory(new AudioText
        {
            Text = "The cars are making allot of noise."
        });

        MakeAllChildrenVisible();
    }

    public override AudioText ResumeDescription { get; } = new()
    {
        Text = "You are back in the city, next to the crossroads. The cars are making allot of noise."
    };
}