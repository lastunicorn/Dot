using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Application.LoadGame;

namespace DustInTheWind.Dot.Application.Actions
{
    public class LoadGameAction : ActionBase
    {
        private readonly IUseCaseFactory useCaseFactory;

        public LoadGameAction(IUseCaseFactory useCaseFactory)
            : base("load")
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public override string Description => "Loads a previously saved game.";

        public override List<string> Usage => new List<string> { "<<:load>>" };

        public override ActionType ActionType => ActionType.EnvironmentCommand;

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*:load\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };
        }

        protected override string[] ExtractParameters(Match match)
        {
            return new string[0];
        }

        public override IEnumerable Execute(params object[] parameters)
        {
            LoadGameUseCase useCase = useCaseFactory.Create<LoadGameUseCase>();
            useCase.Execute();

            //moduleEngine.RequestToChangeModule(ModuleId.Load);

            yield break;
        }
    }
}