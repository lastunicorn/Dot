using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.AdventureGame.Verbs;

namespace DustInTheWind.Dot.AdventureGame.ActionModel
{
    public class ActionSet : HashSet<ActionBase>
    {
        public ActionBase Default => this.First(x => x is LookAtAction);

        public ActionInfo? FindMatchingAction(string command)
        {
            return this
                .Select(x => x.Parse(command))
                .FirstOrDefault(x => x != null);
        }
    }
}