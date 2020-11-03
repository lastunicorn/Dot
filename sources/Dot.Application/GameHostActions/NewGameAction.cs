using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Application.UseCases.NewGame;

namespace DustInTheWind.Dot.Application.GameHostActions
{
    public class NewGameAction : ActionBase
    {
        private readonly IUseCaseFactory useCaseFactory;

        public NewGameAction(IUseCaseFactory useCaseFactory)
            : base("new")
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public override string Description => "Starts a new game.";

        public override List<string> Usage => new List<string> { "<<:new>>" };

        public override ActionType ActionType => ActionType.EnvironmentCommand;

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*:new\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };
        }

        protected override string[] ExtractParameters(Match match)
        {
            return new string[0];
        }

        public override IEnumerable Execute(params object[] parameters)
        {
            CreateNewGameUseCase useCase = useCaseFactory.Create<CreateNewGameUseCase>();
            useCase.Execute();

            yield break;
        }
    }
}