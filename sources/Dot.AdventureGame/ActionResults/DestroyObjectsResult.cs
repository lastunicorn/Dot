using System.Collections.Generic;
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace DustInTheWind.Dot.AdventureGame.ActionResults
{
    public class DestroyObjectsResult
    {
        public IEnumerable<IObject> Objects { get; set; }
    }
}