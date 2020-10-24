using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResultHandlers;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame
{
    public class ResultHandlersCollection
    {
        private readonly Dictionary<Type, IResultHandler> resultHandlers;

        public ResultHandlersCollection(IUserInterface userInterface, GameBase game)
        {
            if (userInterface == null) throw new ArgumentNullException(nameof(userInterface));
            if (game == null) throw new ArgumentNullException(nameof(game));

            resultHandlers = new Dictionary<Type, IResultHandler>
            {
                { typeof(ChangeLocationResult), new ChangeLocationResultHandler(game) },
                { typeof(AskCodeResult), new AskCodeResultHandler() },
                { typeof(DestroyObjectsResult), new DestroyObjectsResultHandler() },
                { typeof(AcquireObjectsResult), new AcquireObjectsResultHandler(game) },
                { typeof(HelpResult), new HelpResultHandler(userInterface) },
                { typeof(StoryBlock), new StoryBlockHandler(userInterface) },
                { typeof(string), new StringHandler(userInterface) },
                { typeof(IEnumerable<string>), new StringsHandler(userInterface) },
                { typeof(IAudioTextEnumerable), new AudioTextHandler(userInterface) },
                { typeof(SuggestionBlock), new SuggestionBlockHandler(userInterface) },
                { typeof(Action), new ActionHandler() }
            };
        }

        public void ProcessActionResults(IEnumerable actionResults)
        {
            foreach (object actionResult in actionResults)
            {
                Type actionResultType = actionResult.GetType();
                IResultHandler resultHandler = ChooseResultHandler(actionResultType);
                resultHandler?.Handle(actionResult);
            }
        }

        private IResultHandler ChooseResultHandler(Type actionResultType)
        {
            bool containsType = resultHandlers.ContainsKey(actionResultType);

            if (containsType)
                return resultHandlers[actionResultType];

            Type actualKey = resultHandlers.Keys
                .FirstOrDefault(x => x.IsAssignableFrom(actionResultType));

            if (actualKey != null)
                return resultHandlers[actualKey];

            return null;
        }
    }
}