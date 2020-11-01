using System;
using System.Collections;
using System.Collections.Generic;
using DustInTheWind.Dot.AdventureGame;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResultHandlers;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.Application.Actions;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Domain.ModuleModel;
using DustInTheWind.Dot.Presentation.Presenters;
using DustInTheWind.Dot.Presentation.Views;

namespace DustInTheWind.Dot.Presentation.Modules
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
     * objects       +   :objects                                           -   ILocation : IObject
     *               +   :o
     * 
     * inventory     +   :inventory                                         -   Inventory
     *               +   :i
     * 
     * actions       +   :actions                                           -   ---
     *               +   :a
     * 
     * menu          +   :menu
     *               +   :m
     * 
     * new           -   :new
     * 
     * load          -   :load
     * 
     * save          -   :save
     * 
     * exit          +   :exit
     *               +   :quit
     *               +   :x
     */

    public class GameModule : IModule
    {
        private readonly IScreenFactory screenFactory;
        private GamePresenter gamePresenter;

        public string Id { get; } = "game";

        public GameModule(IScreenFactory screenFactory)
        {
            this.screenFactory = screenFactory ?? throw new ArgumentNullException(nameof(screenFactory));
        }

        public string Run()
        {
            gamePresenter = screenFactory.Create<GamePresenter>();
            gamePresenter.Display();

            return null;
        }

        public void RequestExit()
        {
            gamePresenter.RequestExit();
        }
    }
}