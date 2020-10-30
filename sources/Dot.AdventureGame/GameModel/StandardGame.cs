using DustInTheWind.Dot.AdventureGame.Actions;
using DustInTheWind.Dot.AdventureGame.Verbs;

namespace DustInTheWind.Dot.AdventureGame.GameModel
{
    public abstract class StandardGame : GameBase
    {
        protected StandardGame()
        {
            Actions.Add(new LookAroundAction(this));
            Actions.Add(new LookAtAction());
            Actions.Add(new WalkAction());
            Actions.Add(new OpenAction());
            Actions.Add(new CloseAction());
            Actions.Add(new PushAction());
            Actions.Add(new PullAction());
            Actions.Add(new MoveAction());
            Actions.Add(new TakeAction());
            Actions.Add(new UseAction());
            Actions.Add(new ReadAction());

            Actions.Add(new ObjectsAction(this));
            Actions.Add(new InventoryAction(Inventory));
            Actions.Add(new VerbsAction(Actions));
            Actions.Add(new HelpAction(Actions));
        }
    }
}