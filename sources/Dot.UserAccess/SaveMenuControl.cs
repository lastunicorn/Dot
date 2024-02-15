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

using DustInTheWind.Dot.Ports.UserAccess;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls;

namespace DustInTheWind.Dot.UserAccess;

internal class SaveMenuControl : SelectableMenu<GameSlotId>
{
    private const int TotalSlotCount = 10;

    public void Populate(IEnumerable<GameSlotId> gameSlotIds)
    {
        IEnumerable<IMenuItem<GameSlotId>> menuItems = CreateMenuItems(gameSlotIds);
        AddRange(menuItems);
    }

    private static IEnumerable<IMenuItem<GameSlotId>> CreateMenuItems(IEnumerable<GameSlotId> gameSlotIds)
    {
        int count = 0;

        foreach (GameSlotId gameSlotId in gameSlotIds)
        {
            while (count < gameSlotId.Id - 1 && count < TotalSlotCount)
            {
                count++;
                yield return CreateEmptyMenuItem(count);
            }

            if (count == TotalSlotCount)
                yield break;

            count++;
            yield return CreateMenuItem(gameSlotId);
        }

        while (count < TotalSlotCount)
        {
            count++;
            yield return CreateEmptyMenuItem(count);
        }

        yield return new SpaceMenuItem<GameSlotId>();

        yield return CreateCancelMenuItem();
    }

    private static IMenuItem<GameSlotId> CreateMenuItem(GameSlotId gameSlotId)
    {
        return new YesNoMenuItem<GameSlotId>
        {
            Text = $"Saved Game {gameSlotId.Id}",
            Value = gameSlotId,
            HorizontalAlign = HorizontalAlign.Center,
            QuestionText = "Overwrite?"
        };
    }

    private static IMenuItem<GameSlotId> CreateEmptyMenuItem(int id)
    {
        return new LabelMenuItem<GameSlotId>
        {
            Text = $"< Empty Slot {id} >",
            Value = new GameSlotId(id),
            HorizontalAlign = HorizontalAlign.Center
        };
    }

    private static IMenuItem<GameSlotId> CreateCancelMenuItem()
    {
        return new LabelMenuItem<GameSlotId>
        {
            Text = "Cancel",
            Value = null,
            HorizontalAlign = HorizontalAlign.Center,
            Key = ConsoleKey.Escape
        };
    }
}