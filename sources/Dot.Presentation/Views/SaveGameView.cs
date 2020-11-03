using System;
using System.Collections.Generic;
using DustInTheWind.Dot.Application.UseCases.SaveGame;
using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.ConsoleHelpers.ConsoleUtil;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain.SaveModel;
using DustInTheWind.Dot.Presentation.Controls;

namespace DustInTheWind.Dot.Presentation.Views
{
    public class SaveGameView : ViewBase, ISaveGameView
    {
        public SaveGameView(Audio audio)
            : base(audio)
        {
        }

        public GameSlot SelectGameSlot(IEnumerable<GameSlot> gameSlots)
        {
            CustomConsole.WriteLine("Save Game", ConsoleColor.Green, HorizontalAlign.Center);
            CustomConsole.WriteLine();

            SaveMenuControl menuControl = new SaveMenuControl();

            menuControl.Populate(gameSlots);

            return menuControl.Display();
        }
    }
}