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

internal class ParkLocation : LocationBase
{
    public override string Id { get; } = "park";

    public override string Name { get; } = "Park";

    public override void InitializeNew()
    {
        Oak oak = new();
        AddObject(oak);

        NorthRoad northRoad = new();
        AddObject(northRoad);
    }

    public override IEnumerable LookAt()
    {
        yield return CreateDescriptionStory(new AudioTextList
        {
            new AudioText
            {
                Text = "You are standing in the middle of a beautiful park. " +
                       "Next to you there is a tall {{oak}}. " +
                       "You feel that you know that {{oak}} from a distant past, " +
                       "but you cannot remember anything else."
            },
            new AudioText
            {
                Text = "Further, in the distance, to the north ({{north road}}), the city sounds can be heard."
            }
        });

        MakeAllChildrenVisible();
    }

    public override AudioText ResumeDescription { get; } = new()
    {
        Text = "You are back in the middle of the park, next to the tall oak."
    };
}