using System;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class StringHandler : ResultHandlerBase<string>
    {
        private readonly IUserInterface userInterface;

        public StringHandler(IUserInterface userInterface)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        }

        public override void Handle(string text)
        {
            userInterface.DisplayInfo(text);
        }
    }
}