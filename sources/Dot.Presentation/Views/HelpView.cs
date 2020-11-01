using System;
using System.Collections.Generic;
using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.Presentation.Controls;

namespace DustInTheWind.Dot.Presentation.Views
{
    public class HelpView : ViewBase
    {
        private readonly Audio audio;
        private InfoBlock infoBlock1;
        private InfoBlock infoBlock2;
        private InfoBlock infoBlock3;

        public HelpView(Audio audio)
            : base(audio)
        {
            this.audio = audio ?? throw new ArgumentNullException(nameof(audio));

            InitializeControls();
        }

        private void InitializeControls()
        {
            infoBlock1 = new InfoBlock(audio)
            {
                Texts = new List<string>
                {
                    "Game Commands",
                    "While in the game, you are allowed to type commands at the prompter in order to interact with the existing objects.",
                    "The game recognizes a limited number of verbs like  <<look>>, <<take>>, <<push>>, <<pull>>, <<open>>, <<close>>, <<use>>, <<exit>>, etc. Type \"<<verbs>>\" to see the entire list.",
                    "Example: <<look at>> {{door}}\nExample: <<take>> {{jug of water}}"
                }
            };

            infoBlock2 = new InfoBlock(audio)
            {
                Texts = new List<string>
                {
                    "Objects and Inventory",
                    "Two of the most important commands that are useful to remember are:\n- <<o>>, short for <<objects>> - displays the list of discovered objects.\n- <<i>>, short for <<inventory>> - displays the list of objects from pocket."
                }
            };

            infoBlock3 = new InfoBlock(audio)
            {
                Texts = new List<string>
                {
                    "Application Commands",
                    "When you want to type commands addressed to the application that hosts the game, use a semicolon before the command:",
                    "Example: <<:menu>>\nExample: <<:save>>\nExample: <<:exit>>\netc."
                }
            };
        }

        public void DisplayHelpInformation()
        {
            infoBlock1.Display();
            infoBlock2.Display();
            infoBlock3.Display();
        }
    }
}