using System;
using System.Collections;
using System.Collections.Generic;
using DustInTheWind.Dot.AdventureGame;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResultHandlers;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Presentation.GameHostActions;
using DustInTheWind.Dot.Presentation.Views;

namespace DustInTheWind.Dot.Presentation.Presenters
{
    /*
     * Verbs:
     * 
     * open          +   open           door                                -   IOpenable : IObject
     * 
     * close         +   close          door                                -   IOpenable : IObject
     * 
     * use           +   use            key         with     door           -   IReceiver : IObject
     *               +   use            key                                 -   IUsable : IObject
     * 
     * give          +   give           acorn       to       squirrel       -   IReceiver : IObject
     *  
     * push          +   push           box                                 -   IPushable : IObject
     * 
     * pull          +   pull           box                                 -   IPushable : IObject
     * 
     * pick up       +   pick up        key                                 -   ITakeable : IObject
     *               +   take           key
     * 
     * look          +   look                                               -   ILocation : IObject
     *               +   look around
     * 
     * look at       +   look at        key                                 -   IObject
     *               +   look           key
     * 
     * talk to       -   talk to        squirrel                            -   ITalkPartner : IObject
     *               -   talk           squirrel
     * 
     * walk to       +   walk to        road                                -   IExit : IObject
     *               +   walk on        road
     *               +   walk           door
     * 
     * objects       +   objects                                            -   ILocation : IObject
     *               +   o
     * 
     * inventory     +   inventory                                          -   Inventory
     *               +   i
     * 
     * menu          +   :menu
     *               +   :m
     * 
     * load          +   :load
     * 
     * save          +   :save
     * 
     * exit          +   :exit
     *               +   :quit
     *               +   :x
     */

    public class GamePresenter
    {
        private readonly GameView gameView;
        private readonly Game game;
        private readonly ResultHandlersCollection resultHandlers;

        private volatile bool exitWasRequested;

        private readonly ActionSet actions = new();

        public GamePresenter(GameView gameView, Game game, ResultHandlersCollection resultHandlers, RequestBus requestBus)
        {
            this.gameView = gameView ?? throw new ArgumentNullException(nameof(gameView));
            this.game = game ?? throw new ArgumentNullException(nameof(game));
            this.resultHandlers = resultHandlers ?? throw new ArgumentNullException(nameof(resultHandlers));

            this.resultHandlers.Add(typeof(ChangeLocationResult), typeof(ChangeLocationResultHandler));
            this.resultHandlers.Add(typeof(AskCodeResult), typeof(AskCodeResultHandler));
            this.resultHandlers.Add(typeof(DestroyObjectsResult), typeof(DestroyObjectsResultHandler));
            this.resultHandlers.Add(typeof(AcquireObjectsResult), typeof(AcquireObjectsResultHandler));
            this.resultHandlers.Add(typeof(HelpResult), typeof(HelpResultHandler));
            this.resultHandlers.Add(typeof(StoryBlock), typeof(StoryBlockHandler));
            this.resultHandlers.Add(typeof(string), typeof(StringHandler));
            this.resultHandlers.Add(typeof(IEnumerable<string>), typeof(StringsHandler));
            this.resultHandlers.Add(typeof(IAudioText), typeof(AudioTextHandler));
            this.resultHandlers.Add(typeof(SuggestionBlock), typeof(SuggestionBlockHandler));
            this.resultHandlers.Add(typeof(Action), typeof(ActionHandler));


            actions.Add(new MainMenuAction(requestBus));
            actions.Add(new ExitAction(requestBus));
            actions.Add(new NewGameAction(requestBus));
            actions.Add(new LoadGameAction(requestBus));
            actions.Add(new SaveGameAction(requestBus));
        }

        public void Display()
        {
            exitWasRequested = false;

            game.CurrentLocationChanged += HandleCurrentLocationChanged;
            game.Open();

            try
            {
                while (!exitWasRequested)
                {
                    string command = gameView.GetUserCommand();
                    ExecuteCommand(command);
                }
            }
            finally
            {
                game.Close();
                game.CurrentLocationChanged -= HandleCurrentLocationChanged;
            }
        }

        private void HandleCurrentLocationChanged(object sender, EventArgs e)
        {
            if (sender is Game game)
                gameView.PrompterText = game.CurrentLocation.Name;
        }

        private void ExecuteCommand(string commandText)
        {
            ActionInfo? actionInfo = game?.FindMatchingAction(commandText) ?? actions.FindMatchingAction(commandText);

            if (actionInfo.HasValue)
            {
                IEnumerable actionResults = actionInfo.Value.ExecuteAction();
                resultHandlers.ProcessActionResults(actionResults);
            }
            else
            {
                AudioText audioText = new UnknownActionAudioText();
                resultHandlers.ProcessActionResults(audioText);
            }
        }

        public void RequestExit()
        {
            exitWasRequested = true;
        }
    }
}