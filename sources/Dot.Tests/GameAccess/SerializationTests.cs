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
using Newtonsoft.Json;
using Xunit;

namespace Dot.Tests.GameAccess;

public class SerializationTests
{
    [Fact]
    public void Test()
    {
        List<IAnimal> animals = new()
        {
            new Cat
            {
                Name = "Gigi",
                Sound = "miau",
                Color = "black"
            },
            new Dog
            {
                Name = "Azog",
                Sound = "How-how",
                TailSize = "big"
            }
        };

        JsonSerializerSettings settings = new()
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        string json = JsonConvert.SerializeObject(animals, Formatting.Indented, settings);

        List<IAnimal> animals2 = JsonConvert.DeserializeObject<List<IAnimal>>(json, settings);
    }
}

public interface IAnimal
{
    string Name { get; set; }

    string Sound { get; set; }
}

public class Cat : IAnimal
{
    public string Name { get; set; }

    public string Sound { get; set; }

    public string Color { get; set; }
}

public class Dog : IAnimal
{
    public string Name { get; set; }

    public string Sound { get; set; }

    public string TailSize { get; set; }
}