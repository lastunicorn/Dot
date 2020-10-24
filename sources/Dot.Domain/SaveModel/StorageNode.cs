using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DustInTheWind.Dot.Domain.SaveModel
{
    [Serializable]
    public class StorageNode : Dictionary<string, object>
    {
        public StorageNode()
        {
        }

        public StorageNode(SerializationInfo info, StreamingContext context)
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
    }
}