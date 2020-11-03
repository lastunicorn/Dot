using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.AdventureGame.Actions
{
    public class HelpAction : ActionBase
    {
        private readonly ActionSet actions;

        public override string Description => "Displays details about an action. It may be a verb (that you use in the game, like \"look\", \"talk\", etc) or a command (to control the game, like \"menu\", \"save\", etc).";

        public override List<string> Usage => new List<string> { "<<help>> <<<verb> >>", "<<help>> <<<command> >>" };

        public override ActionType ActionType => ActionType.GameCommand;

        public HelpAction(ActionSet actions)
            : base("help")
        {
            this.actions = actions ?? throw new ArgumentNullException(nameof(actions));
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*help\s*(\s(?'verb'.+))\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };
        }

        protected override string[] ExtractParameters(Match match)
        {
            return new[]
            {
                match.Groups["verb"].Value
            };
        }

        public override IEnumerable Execute(params object[] parameters)
        {
            if (parameters == null || parameters.Length <= 1)
                return GeneralHelp();

            string[] verbName = parameters
                .OfType<string>()
                .ToArray();

            return Execute(verbName);
        }

        private IEnumerable GeneralHelp()
        {
            yield return new SuggestionBlock
            {
                Texts = new List<string>
                {
                    "The game recognizes a limited number of verbs:  " + GetVerbNames() + ".",
                    "To see the complete list of verbs, type <<verbs>> or just <<v>>."
                }
            };

            yield return new SuggestionBlock
            {
                Texts = new List<string>
                {
                    "Objects that you take from the room goes into your pocket. Type <<inventory>> or just <<i>> to see that list.",
                    "The list of objects that you already discovered in the room can be displayed by typing <<objects>> or just <<o>>.",
                    "Note: <<o>> and <<i>> are two of the most useful commands. Remember them."
                }
            };

            yield return new SuggestionBlock
            {
                Texts = new List<string>
                {
                    "When you want to type commands addressed to the host of the game, use a semicolon \":\" before the command:",
                    "Example: <<:menu>>, <<:save>>, <<:load>>, <<:exit>>."
                }
            };
        }

        private string GetVerbNames()
        {
            IEnumerable<string> verbNames = actions
                .Where(x => x.ActionType == ActionType.Verb)
                .Select(x => "<<" + x.Name + ">>");

            return string.Join(", ", verbNames);
        }

        private IEnumerable Execute(string[] verbNames)
        {
            IEnumerable<HelpItemViewModel> actionsToDisplay = actions
                .Where(x => x.Names.Any(verbNames.Contains))
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