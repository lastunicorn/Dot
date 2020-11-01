using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.GameStorage.Binary
{
    [Serializable]
    public class BinaryStorageNode : Dictionary<string, object>
    {
        public Type ObjectType { get; set; }

        public List<BinaryStorageNode> Children { get; } = new List<BinaryStorageNode>();

        public BinaryStorageNode()
        {
        }

        public BinaryStorageNode(StorageNode storageNode)
        {
            ObjectType = storageNode.ObjectType;

            IEnumerable<BinaryStorageNode> children = storageNode.Children
                .Select(x => new BinaryStorageNode(x));

            Children.AddRange(children);

            foreach ((string key, object value) in storageNode)
            {
                if (value is StorageNode childStorageNode)
                {
                    BinaryStorageNode childBinaryStorageNode = new BinaryStorageNode(childStorageNode);
                    Add(key, childBinaryStorageNode);
                }
                else
                {
                    Add(key, value);
                }
            }
        }

        public BinaryStorageNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public T Get<T>(string key, T defaultValue = default)
        {
            if (!ContainsKey(key))
                return defaultValue;

            object rawValue = this[key];

            return (T)Convert.ChangeType(rawValue, typeof(T));
        }

        public void Set<T>(string key, T value)
        {
            this[key] = value;
        }

        public StorageNode ToEntity()
        {
            StorageNode storageNode = new StorageNode
            {
                ObjectType = ObjectType
            };

            IEnumerable<StorageNode> childStorageNodes = Children
                .Select(x => x.ToEntity());

            storageNode.Children.AddRange(childStorageNodes);

            foreach ((string key, object value) in this)
            {
                if (value is BinaryStorageNode childBinaryStorageNode)
                {
                    StorageNode childStorageNode = childBinaryStorageNode.ToEntity();
                    storageNode.Add(key, childStorageNode);
                }
                else
                {
                    storageNode.Add(key, value);
                }
            }

            return storageNode;
        }
    }
}