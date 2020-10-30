using System;
using System.Collections.Generic;
using DustInTheWind.Dot.Application.LoadGame;
using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.ConsoleHelpers.ConsoleUtil;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.SaveModel;
using DustInTheWind.Dot.Presentation.Controls;

namespace DustInTheWind.Dot.Presentation.Views
{
    public class LoadGameView : ViewBase, ILoadGameView
    {
        public LoadGameView(Audio audio)
            : base(audio)
        {
        }

        public bool AskToSavePreviousGame()
        {
            return true;
        }

        public GameSlot AskToChooseGameSlot(IEnumerable<GameSlot> gameSlots)
        {
            CustomConsole.WriteLine("Load Game", ConsoleColor.Green, HorizontalAlign.Center);
            CustomConsole.WriteLine();

            LoadMenuControl menuControl = new LoadMenuControl();

            menuControl.Populate(gameSlots);

            return menuControl.Display();
        }

        public void DisplaySuccessMessage()
        {
            DisplayInfo("Game loaded");
        }
    }
}