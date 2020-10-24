using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResults;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class AskCodeResultHandler : ResultHandlerBase<AskCodeResult>
    {
        public override void Handle(AskCodeResult askCodeResult)
        {
            askCodeResult.Code = ReadCode(askCodeResult.PrompterText);
        }

        public string ReadCode(string prompterText)
        {
            // todo
            // return CommandLinePrompter.QuickDisplay(prompterText);

            return string.Empty;
        }
    }
}