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
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Demo.Objects;

internal class Oak : ContainerObject
{
    public override string Id { get; } = "oak";

    public override string Name { get; } = "oak";

    public override string ImagePath { get; } = "DustInTheWind.Dot.Demo.Ascii.oak.ascii";

    public Oak()
    {
        Acorn acorn = new();
        AddObject(acorn);
    }

    public override IEnumerable LookAt()
    {
        yield return CreateDescriptionStory(new AudioText
        {
            Text = "A tall oak. On the lowest branch there is a big {{acorn}}."
        });

        MakeAllChildrenVisible();
    }
}