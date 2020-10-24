using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace DustInTheWind.Dot.AdventureGame.Actions
{
    public class InventoryAction : ActionBase
    {
        private readonly Inventory inventory;

        public override string Description => "Displays a list with the objects from your pocket.";

        public override List<string> Usage => new List<string> { "<<inventory>>", "<<i>>" };

        public override ActionType ActionType => ActionType.GameCommand;

        public InventoryAction(Inventory inventory)
            : base("inventory", "i")
        {
            this.inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        protected override List<Regex> CreateMatchers()
        {
            return new List<Regex>
            {
                new Regex(@"^\s*(inventory|i)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };
        }

        protected override string[] ExtractParameters(Match match)
        {
            return new string[0];
        }

        public override IEnumerable Execute(params object[] parameters)
        {
            foreach (IObject @object in inventory)
                @object.IsVisible = true;

            string objectNames = GetInventoryObjectsNames();

            if (string.IsNullOrEmpty(objectNames))
                yield return "Inventory: <empty>";
            else
                yield return "Inventory: {{" + objectNames + "}}";
        }

        private string GetInventoryObjectsNames()
        {
            if (inventory == null)
                return null;

            List<string> names = inventory
                .Select(x =>
                {
                    IObjectContainer objectContainer = x as IObjectContainer;

                    if (objectContainer == null)
                        return x.Name;

                    string childrenNames = objectContainer.GetChildrenNames();

                    if (string.IsNullOrEmpty(childrenNames))
                        return x.Name;

                    return x.Name + " (" + childrenNames + ")";
                })
                .ToList();

            return string.Join(", ", names);
        }
    }
}