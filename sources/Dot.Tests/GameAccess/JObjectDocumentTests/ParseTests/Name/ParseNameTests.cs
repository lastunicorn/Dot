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

using System.IO;
using DustInTheWind.Dot.GameAccess;
using FluentAssertions;
using Xunit;

namespace Dot.Tests.GameAccess.JObjectDocumentTests.ParseTests.Name;

public class ParseNameTests
{
    [Fact]
    public void HavingFileContainingName_WhenParsed_ThenNamePropertyContainsThatName()
    {
        JObjectDocument jObjectDocument = ParseFile("name-01-object-with-name.json");
        jObjectDocument.Name.Should().Be("flower");
    }

    [Fact]
    public void HavingFileContainingNoName_WhenParsed_ThenNamePropertyIsNull()
    {
        JObjectDocument jObjectDocument = ParseFile("name-02-object-with-no-name.json");
        jObjectDocument.Name.Should().BeNull();
    }

    private static JObjectDocument ParseFile(string resourceFileName)
    {
        using Stream stream = EmbeddedResources.GetResourceStream(resourceFileName);
        return JObjectDocument.Parse(stream);
    }
}