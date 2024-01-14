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

using Newtonsoft.Json;

namespace Dot.GameAccess;

public class JObjectDocument
{
    public string Name { get; set; }

    public string[] Names { get; set; }

    public string Id { get; set; }

    public string Type { get; set; }

    public string[] Objects { get; set; }

    public string[] States { get; set; }

    public string[] CharacterStates { get; set; }

    public JObjectProperty[] Properties { get; set; }

    public JObjectAction[] Actions { get; set; }

    public static JObjectDocument Parse(Stream stream)
    {
        using StreamReader streamReader = new(stream);
        string json = streamReader.ReadToEnd();
        return JsonConvert.DeserializeObject<JObjectDocument>(json);
    }
}