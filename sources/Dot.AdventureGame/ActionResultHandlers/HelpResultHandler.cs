using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class HelpResultHandler : ResultHandlerBase<HelpResult>
    {
        private readonly IUserInterface userInterface;

        public HelpResultHandler(IUserInterface userInterface)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        }

        public override void Handle(HelpResult result)
        {
            DisplayHelpItems(result.HelpItems);
        }

        private void DisplayHelpItems(IEnumerable<HelpItemViewModel> helpItems)
        {
            bool isFirstAction = true;

            foreach (HelpItemViewModel helpItem in helpItems)
            {
                List<Tuple<string, string, string>> list = BuildHelpTextFor(helpItem);

                int topMargin = isFirstAction ? 2 : 0;
                isFirstAction = false;

                const int bottomMargin = 2;

                userInterface.Display(list, 14, topMargin, bottomMargin);

                Thread.Sleep(50);
            }
        }

        private static List<Tuple<string, string, string>> BuildHelpTextFor(HelpItemViewModel helpItem)
        {
            string actionName = BuildActionName(helpItem);

            List<Tuple<string, string, string>> list = new List<Tuple<string, string, string>>
            {
                new Tuple<string, string, string>(actionName, "-", helpItem.Description)
            };

            if (helpItem.Usage != null)
            {
                IEnumerable<Tuple<string, string, string>> rows = helpItem.Usage
                    .Select(x => new Tuple<string, string, string>(string.Empty, "-", "Usage: " + x));

                list.AddRange(rows);
            }

            return list;
        }

        private static string BuildActionName(HelpItemViewModel helpItem)
        {
            bool isEnvironmentCommand = helpItem.ActionType == ActionType.EnvironmentCommand;

            if (isEnvironmentCommand)
                return "<<:" + helpItem.Name + ">>";

            return "<<" + helpItem.Name + ">>";
        }
    }
}