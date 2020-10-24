using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public static class ObjectContainerExtensions
    {
        public static string GetChildrenNames(this IObjectContainer objectContainer)
        {
            List<string> names = objectContainer
                .Where(x => x.IsVisible)
                .Select(x =>
                {
                    IObjectContainer o = x as IObjectContainer;

                    if (o == null)
                        return x.Name;

                    string childrenNames = o.GetChildrenNames();

                    if (string.IsNullOrEmpty(childrenNames))
                        return x.Name;

                    return x.Name + " (" + childrenNames + ")";
                })
                .ToList();

            return string.Join(", ", names);
        }
    }
}