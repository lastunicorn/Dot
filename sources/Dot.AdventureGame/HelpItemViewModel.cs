using System.Collections.Generic;
using DustInTheWind.Dot.AdventureGame.ActionModel;

namespace DustInTheWind.Dot.AdventureGame
{
    public class HelpItemViewModel
    {
        public ActionType ActionType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> Usage { get; set; }
    }
}