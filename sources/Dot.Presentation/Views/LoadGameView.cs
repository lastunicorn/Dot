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

using System;
using System.Collections.Generic;
using DustInTheWind.Dot.Application.UseCases.LoadGame;
using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.ConsoleUtil;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Presentation.Controls;

namespace DustInTheWind.Dot.Presentation.Views;

public class LoadGameView : ViewBase, ILoadGameView
{
    private readonly Audio audio;

    public LoadGameView(Audio audio)
        : base(audio)
    {
        this.audio = audio ?? throw new ArgumentNullException(nameof(audio));
    }

    public bool AskToSavePreviousGame()
    {
        return true;
    }

    public GameSlotId AskToChooseGameSlot(IEnumerable<GameSlotId> gameSlotIds)
    {
        CustomConsole.WriteLine("Load Game", ConsoleColor.Green, HorizontalAlign.Center);
        CustomConsole.WriteLine();

        LoadMenuControl menuControl = new();

        menuControl.Populate(gameSlotIds);

        return menuControl.Display();
    }

    public void AnnounceSuccess()
    {
        InfoBlock infoBlock = new(audio)
        {
            Text = "Game loaded"
        };
        infoBlock.Display();
    }
}