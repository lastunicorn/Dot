using System;
using System.Collections.Generic;
using DustInTheWind.Dot.Application.UseCases.LoadGame;
using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.Ports.GameSavesAccess;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.ConsoleUtil;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Presentation.Controls;

namespace DustInTheWind.Dot.Presentation.Views
{
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

        public GameSlot AskToChooseGameSlot(IEnumerable<GameSlot> gameSlots)
        {
            CustomConsole.WriteLine("Load Game", ConsoleColor.Green, HorizontalAlign.Center);
            CustomConsole.WriteLine();

            LoadMenuControl menuControl = new LoadMenuControl();

            menuControl.Populate(gameSlots);

            return menuControl.Display();
        }

        public void AnnounceSuccess()
        {
            InfoBlock infoBlock = new InfoBlock(audio)
            {
                Text = "Game loaded"
            };
            infoBlock.Display();
        }
    }
}