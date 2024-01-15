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

using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.AdventureGame.ExportModel;
using DustInTheWind.Dot.Ports.GameSavesAccess;

namespace DustInTheWind.Dot.Application;

internal static class StorageDataExtensions
{
    public static StorageData ToStorageData(this ExportData exportData)
    {
        StorageData storageData = new()
        {
            Version = exportData.Version,
            SaveTime = exportData.SaveTime,
            ObjectType = exportData.ObjectType,
        };

        foreach ((string key, object value) in exportData)
            storageData.Add(key, value);

        IEnumerable<StorageNode> storageNodeChildren = exportData.Children
            .Select(x => x.ToStorageNode());

        storageData.Children.AddRange(storageNodeChildren);


        return storageData;
    }

    private static StorageNode ToStorageNode(this ExportNode exportNode)
    {
        StorageNode storageNode = new()
        {
            ObjectType = exportNode.ObjectType
        };

        foreach ((string key, object value) in exportNode)
            storageNode.Add(key, value);

        IEnumerable<StorageNode> storageNodeChildren = exportNode.Children
            .Select(x => x.ToStorageNode());

        storageNode.Children.AddRange(storageNodeChildren);

        return storageNode;
    }

    public static ExportData ToExportData(this StorageData storageData)
    {
        ExportData exportData = new()
        {
            Version = storageData.Version,
            SaveTime = storageData.SaveTime,
            ObjectType = storageData.ObjectType,
        };

        foreach ((string key, object value) in storageData)
            exportData.Add(key, value);

        IEnumerable<ExportNode> exportNodeChildren = storageData.Children
            .Select(x => x.ToExportNode());

        exportData.Children.AddRange(exportNodeChildren);


        return exportData;
    }

    private static ExportNode ToExportNode(this StorageNode storageNode)
    {
        ExportNode exportNode = new()
        {
            ObjectType = storageNode.ObjectType
        };

        foreach ((string key, object value) in storageNode)
            exportNode.Add(key, value);

        IEnumerable<ExportNode> exportNodeChildren = storageNode.Children
            .Select(x => x.ToExportNode());

        exportNode.Children.AddRange(exportNodeChildren);

        return exportNode;
    }
}