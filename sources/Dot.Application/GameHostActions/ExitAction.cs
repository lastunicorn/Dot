using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.Application.GameHostActions
{
    public class ExitAction : ActionBase
    {
        private readonly IGameApplication gameApplication;

        public ExitAction(IGameApplication gameApplication)
            : base("exit", "quit", "x")
        {
            this.gameApplication = gameApplication ?? throw new ArgumentNullException(nameof(gameApplication));
        }

        public override string Description => "Exits the game.";

        public override List<string> Usage => new List<string> { "<<:exit>>", "<<:quit>>", "<<:x>>" };

        public override ActionType ActionType => ActionType.EnvironmentCommand;

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*(:exit|:quit|:x)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };
        }

        protected override string[] ExtractParameters(Match match)
        {
            return new string[0];
        }

        public override IEnumerable Execute(params object[] parameters)
        {
            gameApplication.Close();

            yield break;
        }
    }
}