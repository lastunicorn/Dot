using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResults;

namespace DustInTheWind.Dot.AdventureGame.Actions
{
    public class VerbsAction : ActionBase
    {
        private readonly ActionSet actions;

        public override string Description => "Displays the list of verbs you can use.";

        public override List<string> Usage => new List<string> { "<<verbs>>", "<<v>>" };

        public override ActionType ActionType => ActionType.GameCommand;

        public VerbsAction(ActionSet actions)
            : base("verbs", "v")
        {
            this.actions = actions ?? throw new ArgumentNullException(nameof(actions));
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*(verbs|v)\s*\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };
        }

        protected override string[] ExtractParameters(Match match)
        {
            return new string[0];
        }

        public override IEnumerable Execute(params object[] parameters)
        {
            IEnumerable<HelpItemViewModel> actionsToDisplay = actions
                .Select(x => new HelpItemViewModel
                {
                    ActionType = x.ActionType,
                    Name = x.Name,
                    Description = x.Description,
                    Usage = x.Usage
                });

            yield return new HelpResult
            {
                HelpItems = actionsToDisplay.ToList()
            };
        }
    }
}