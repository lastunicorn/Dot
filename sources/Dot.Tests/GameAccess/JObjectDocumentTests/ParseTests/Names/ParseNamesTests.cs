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

namespace Dot.Tests.GameAccess.JObjectDocumentTests.ParseTests.Names;

public class ParseNamesTests
{
    [Fact]
    public void HavingFileContainingOneNameInArray_WhenParsed_ThenNamesArrayContainsOnlyThatName()
    {
        JObjectDocument jObjectDocument = ParseFile("names-01-object-with-one-name.json");

        string[] expected = { "flower" };
        jObjectDocument.Names.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void HavingFileContainingTwoNamesInArray_WhenParsed_ThenNamesArrayContainsBothNames()
    {
        JObjectDocument jObjectDocument = ParseFile("names-02-object-with-two-names.json");

        string[] expected = { "flower", "red flower" };
        jObjectDocument.Names.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    [Fact]
    public void HavingFileContainingNoNamesInArray_WhenParsed_ThenNamesArrayIsEmpty()
    {
        JObjectDocument jObjectDocument = ParseFile("names-03-object-with-no-names-in-array.json");

        jObjectDocument.Names.Should().BeEmpty();
    }

    [Fact]
    public void HavingFileContainingNoNamesArray_WhenParsed_ThenNamesArrayIsNull()
    {
        JObjectDocument jObjectDocument = ParseFile("names-04-object-with-no-names-array.json");

        jObjectDocument.Names.Should().BeNull();
    }

    private static JObjectDocument ParseFile(string resourceFileName)
    {
        using Stream stream = EmbeddedResources.GetResourceStream(resourceFileName);
        return JObjectDocument.Parse(stream);
    }
}