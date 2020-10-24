using System;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class SuggestionBlockHandler : ResultHandlerBase<SuggestionBlock>
    {
        private readonly IUserInterface userInterface;

        public SuggestionBlockHandler(IUserInterface userInterface)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        }

        public override void Handle(SuggestionBlock suggestionBlock)
        {
            userInterface.DisplaySuggestion(suggestionBlock);
        }
    }
}