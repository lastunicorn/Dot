using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.AudioSupport;

namespace DustInTheWind.Dot.Presentation.Views
{
    public class HelpView : ViewBase, IHelpView
    {
        public HelpView(Audio audio)
            : base(audio)
        {
        }

        public void DisplayHelpInformation()
        {
            DisplayInfoBlock(new[]
            {
                "While in the game, you are allowed to type commands at the prompter in order to interact with the existing objects.",
                "The game recognizes a limited number of verbs like  <<look>>, <<take>>, <<push>>, <<pull>>, <<open>>, <<close>>, <<use>>, <<exit>>, etc. Type \"<<verbs>>\" to see the entire list.",
                "Example: <<look at>> {{door}}\nExample: <<take>> {{jug of water}}",
                "Note: The <<look at>> verb is optional. If the name of the object is typed at the prompter, the <<look at>> verb is implied.",
                "Example: {{door}}",
                "Two of the most important commands that are useful to remember are:\n- <<o>>, short for <<objects>> - displays the list of discovered objects.\n- <<i>>, short for <<inventory>> - displays the list of objects from pocket."
            });

            DisplayInfoBlock(new[]
            {
                "When you want to type commands addressed to the application that hosts the game, use a semicolon before the command:",
                "Example: <<:menu>>\nExample: <<:save>>\nExample: <<:load>>\nExample: <<:exit>>\netc."
            });
        }
    }
}