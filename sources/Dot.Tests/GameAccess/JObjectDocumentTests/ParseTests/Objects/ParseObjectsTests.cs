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

namespace Dot.Tests.GameAccess.JObjectDocumentTests.ParseTests.Objects;

public class ParseObjectsTests
{
    [Fact]
    public void HavingFileContainingOneObjectInArray_WhenParsed_ThenObjectsArrayContainsOnlyThatValue()
    {
        JObjectDocument jObjectDocument = ParseFile("objects-01-object-with-one-object.json");

        string[] expected = { "knife" };
        jObjectDocument.Objects.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void HavingFileContainingTwoObjectsInArray_WhenParsed_ThenObjectsArrayContainsBothValues()
    {
        JObjectDocument jObjectDocument = ParseFile("objects-02-object-with-two-objects.json");

        string[] expected = { "knife", "spoon" };
        jObjectDocument.Objects.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    [Fact]
    public void HavingFileContainingNoObjectsInArray_WhenParsed_ThenObjectsArrayIsEmpty()
    {
        JObjectDocument jObjectDocument = ParseFile("objects-03-object-with-no-objects-in-array.json");

        jObjectDocument.Objects.Should().BeEmpty();
    }

    [Fact]
    public void HavingFileContainingNoObjectsArray_WhenParsed_ThenObjectsArrayIsNull()
    {
        JObjectDocument jObjectDocument = ParseFile("objects-04-object-with-no-objects-array.json");

        jObjectDocument.Objects.Should().BeNull();
    }

    private static JObjectDocument ParseFile(string resourceFileName)
    {
        using Stream stream = EmbeddedResources.GetResourceStream(resourceFileName);
        return JObjectDocument.Parse(stream);
    }
}