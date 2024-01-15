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
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Demo.Objects;

internal class Acorn : ObjectBase, ITakeable
{
    public override string Id => "acorn";

    public override string Name => "acorn";

    public override string ImagePath { get; } = "DustInTheWind.Dot.Demo.Ascii.acorn.ascii";

    public override IEnumerable LookAt()
    {
        yield return CreateDescriptionStory(new AudioText
        {
            Text = "An acorn."
        });
    }

    public IEnumerable Take()
    {
        yield return CreateDescriptionStory(new AudioText
        {
            Text = "You take the acorn and put it in your pocket."
        });

        yield return new AcquireObjectsResult
        {
            Objects = new[] { this }
        };
    }
}