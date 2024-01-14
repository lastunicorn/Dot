using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Ports.GameSavesAccess;

namespace DustInTheWind.Dot.AdventureGame.GameModel
{
    public class AddOnCollection : HashSet<IAddOn>, IExportable
    {
        public StorageNode Export()
        {
            StorageNode storageNode = new StorageNode
            {
                ObjectType = GetType()
            };

            IEnumerable<StorageNode> addOnStorageNodes = this
                .Select(x => x.Export());

            foreach (StorageNode addOnStorageNode in addOnStorageNodes)
                storageNode.Children.Add(addOnStorageNode);

            return storageNode;
        }

        public void Import(StorageNode storageNode)
        {
            foreach (StorageNode addOnNode in storageNode.Children)
            {
                Type addOnType = addOnNode.ObjectType;

                IAddOn addOn = Activator.CreateInstance(addOnType) as IAddOn;
                addOn?.Import(addOnNode);

                Add(addOn);
            }
        }
    }
}