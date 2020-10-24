using System.Collections;

namespace DustInTheWind.Dot.AdventureGame.ActionModel
{
    public struct ActionInfo
    {
        public ActionBase Action { get; set; }

        public object[] Parameters { get; set; }

        public IEnumerable ExecuteAction()
        {
            return Action.Execute(Parameters);
        }
    }
}