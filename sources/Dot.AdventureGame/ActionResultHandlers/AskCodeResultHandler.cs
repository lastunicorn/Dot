using System;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class AskCodeResultHandler : ResultHandlerBase<AskCodeResult>
    {
        private readonly IUserInterface userInterface;

        public AskCodeResultHandler(IUserInterface userInterface)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        }

        public override void Handle(AskCodeResult askCodeResult)
        {
            askCodeResult.Code = ReadCode(askCodeResult.PrompterText);
        }

        public string ReadCode(string prompterText)
        {
            return userInterface.Question(prompterText);
        }
    }
}