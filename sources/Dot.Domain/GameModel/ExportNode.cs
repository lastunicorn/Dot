// Dot
// Copyright (C) 2020-2024 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DustInTheWind.Dot.Domain.GameModel;

[Serializable]
public class ExportNode : Dictionary<string, object>
{
    public Type ObjectType { get; set; }

    public List<ExportNode> Children { get; } = new();

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

    public ExportNode()
    {
    }

    public ExportNode(SerializationInfo info, StreamingContext context)
    {

    }
}