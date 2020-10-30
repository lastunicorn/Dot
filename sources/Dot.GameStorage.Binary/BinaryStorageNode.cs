using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.GameStorage.Binary
{
    [Serializable]
    public class BinaryStorageNode : Dictionary<string, object>
    {
        public Type ObjectType { get; set; }

        public BinaryStorageNode()
        {
        }

        public BinaryStorageNode(StorageNode storageNode)
        {
            ObjectType = storageNode.ObjectType;

            foreach ((string key, object value) in storageNode)
            {
                if (value is StorageNode childStorageNode)
                {
                    BinaryStorageNode binaryStorageNode = new BinaryStorageNode(childStorageNode);
                    Add(key, binaryStorageNode);
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