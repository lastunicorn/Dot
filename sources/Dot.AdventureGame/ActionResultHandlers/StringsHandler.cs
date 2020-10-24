using System;
using System.Collections.Generic;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class StringsHandler : ResultHandlerBase<IEnumerable<string>>
    {
        private readonly IUserInterface userInterface;

        public StringsHandler(IUserInterface userInterface)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        }

        public override void Handle(IEnumerable<string> text)
        {
            userInterface.DisplayInfo(text);
        }
    }
}