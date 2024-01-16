using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.GameModel;

namespace DustInTheWind.Dot.AdventureGame.GameModel
{
    public class AddOnCollection : HashSet<IAddOn>, IExportable
    {
        public ExportNode Export()
        {
            ExportNode exportNode = new ExportNode
            {
                ObjectType = GetType()
            };

            IEnumerable<ExportNode> addOnStorageNodes = this
                .Select(x => x.Export());

            foreach (ExportNode addOnStorageNode in addOnStorageNodes)
                exportNode.Children.Add(addOnStorageNode);

            return exportNode;
        }

        public void Import(ExportNode exportNode)
        {
            foreach (ExportNode addOnNode in exportNode.Children)
            {
                Type addOnType = addOnNode.ObjectType;

                IAddOn addOn = Activator.CreateInstance(addOnType) as IAddOn;
                addOn?.Import(addOnNode);

                Add(addOn);
            }
        }
    }
}