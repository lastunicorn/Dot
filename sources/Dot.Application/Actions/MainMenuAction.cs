using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.ModuleModel;

namespace DustInTheWind.Dot.Application.Actions
{
    public class MainMenuAction : ActionBase
    {
        private readonly ModuleEngine moduleEngine;

        public MainMenuAction(ModuleEngine moduleEngine)
            : base("menu", "m")
        {
            this.moduleEngine = moduleEngine ?? throw new ArgumentNullException(nameof(moduleEngine));
        }

        public override string Description => "Displays the main menu of the game.";

        public override List<string> Usage => new List<string> { "<<:menu>>", "<<:m>>" };

        public override ActionType ActionType => ActionType.EnvironmentCommand;

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*(:menu|:m)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };
        }

        protected override string[] ExtractParameters(Match match)
        {
            return new string[0];
        }

        public override IEnumerable Execute(params object[] parameters)
        {
            moduleEngine.RequestToChangeModule("main-menu");

            yield break;
        }
    }
}