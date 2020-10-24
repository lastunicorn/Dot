using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.AdventureGame.ActionModel;

namespace DustInTheWind.Dot.AdventureGame
{
    public class ResultHandlersCollection
    {
        private readonly IActionResultHandlerFactory actionResultHandlerFactory;
        private readonly Dictionary<Type, Type> resultHandlers = new Dictionary<Type, Type>();

        public ResultHandlersCollection(IActionResultHandlerFactory actionResultHandlerFactory)
        {
            this.actionResultHandlerFactory = actionResultHandlerFactory ?? throw new ArgumentNullException(nameof(actionResultHandlerFactory));
        }

        public void Add(Type actionResultType, Type handlerType)
        {
            if (actionResultType == null) throw new ArgumentNullException(nameof(actionResultType));
            if (handlerType == null) throw new ArgumentNullException(nameof(handlerType));

            resultHandlers.Add(actionResultType, handlerType);
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
            IResultHandler resultHandler = GetHandler(actionResultType);

            if (resultHandler == null)
            {
                Type actualKey = resultHandlers.Keys
                    .FirstOrDefault(x => x.IsAssignableFrom(actionResultType));

                resultHandler = GetHandler(actualKey);
            }

            return resultHandler;
        }

        private IResultHandler GetHandler(Type actionResultType)
        {
            bool containsType = resultHandlers.ContainsKey(actionResultType);

            if (!containsType)
                return null;

            Type resultHandlerType = resultHandlers[actionResultType];
            return actionResultHandlerFactory.Create(resultHandlerType);
        }
    }
}