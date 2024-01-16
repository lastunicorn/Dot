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

using System.Collections.ObjectModel;

namespace DustInTheWind.Dot.GameHosting;

internal class ModuleCollection : Collection<IModule>
{
    public IModule GetById(string id)
    {
        return Items.FirstOrDefault(x => x.Id == id);
    }

    public void AddRange(IEnumerable<IModule> modules)
    {
        foreach (IModule module in modules)
        {
            if (module == null)
                throw new ArgumentException("A null module cannot be added to the collection.", nameof(modules));

            Items.Add(module);
        }
    }
}