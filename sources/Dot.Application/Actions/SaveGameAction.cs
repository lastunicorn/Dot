using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Application.SaveGame;

namespace DustInTheWind.Dot.Application.Actions
{
    public class SaveGameAction : ActionBase
    {
        private readonly IUseCaseFactory useCaseFactory;

        public SaveGameAction(IUseCaseFactory useCaseFactory)
            : base("save")
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public override string Description => "Saves the current game.";

        public override List<string> Usage => new List<string> { "<<:save>>" };

        public override ActionType ActionType => ActionType.EnvironmentCommand;

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*:save\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };
        }

        protected override string[] ExtractParameters(Match match)
        {
            return new string[0];
        }

        public override IEnumerable Execute(params object[] parameters)
        {
            SaveGameUseCase useCase = useCaseFactory.Create<SaveGameUseCase>();
            useCase.Execute();

            //moduleEngine.RequestToChangeModule(ModuleId.Save);

            yield break;
        }
    }
}