using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public class ObjectList : Collection<IObject>
    {
        public string NamesToString()
        {
            switch (Items.Count)
            {
                case 0:
                    return string.Empty;

                case 1:
                    return "a {{" + Items[0].Name + "}}";

                default:
                    IEnumerable<string> items = Items
                        .Take(Items.Count - 1)
                        .Select(x => "a {{" + x.Name + "}}");

                    string allButLast = string.Join(",", items);

                    return allButLast + " and a {{" + Items[^1].Name + "}}";
            }
        }
    }
}