using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.SaveModel;

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
                storageNode.Add("addon", addOnStorageNode);

            return storageNode;
        }

        public void Import(StorageNode storageNode)
        {
            IEnumerable<StorageNode> addOnNodes = storageNode
                .Where(x => x.Key == "addon")
                .Select(x => x.Value)
                .Cast<StorageNode>();

            foreach (StorageNode addOnNode in addOnNodes)
            {
                Type addOnType = addOnNode.ObjectType;

                IAddOn addOn = Activator.CreateInstance(addOnType) as IAddOn;
                addOn?.Import(addOnNode);

                Add(addOn);
            }
        }
    }
}