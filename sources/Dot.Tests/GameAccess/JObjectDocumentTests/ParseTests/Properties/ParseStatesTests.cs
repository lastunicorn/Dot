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

namespace Dot.Tests.GameAccess.JObjectDocumentTests.ParseTests.Properties;

public class ParsePropertiesTests
{
    [Fact]
    public void HavingFileContainingOnePropertyInArray_WhenParsed_ThenPropertiesArrayContainsOnlyThatValue()
    {
        JObjectDocument jObjectDocument = ParseFile("properties-01-object-with-one-property.json");

        JObjectProperty[] expected =
        {
            new()
            {
                Name = "prop1",
                Type = "int"
            }
        };
        jObjectDocument.Properties.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void HavingFileContainingTwoPropertiesInArray_WhenParsed_ThenPropertiesArrayContainsBothValues()
    {
        JObjectDocument jObjectDocument = ParseFile("properties-02-object-with-two-properties.json");

        JObjectProperty[] expected =
        {
            new()
            {
                Name = "prop1",
                Type = "int"
            },
            new()
            {
                Name = "prop2",
                Type = "text"
            }
        };
        jObjectDocument.Properties.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    [Fact]
    public void HavingFileContainingNoPropertiesInArray_WhenParsed_ThenPropertiesArrayIsEmpty()
    {
        JObjectDocument jObjectDocument = ParseFile("properties-03-object-with-no-properties-in-array.json");

        jObjectDocument.Properties.Should().BeEmpty();
    }

    [Fact]
    public void HavingFileContainingNoPropertiesArray_WhenParsed_ThenPropertiesArrayIsNull()
    {
        JObjectDocument jObjectDocument = ParseFile("properties-04-object-with-no-properties-array.json");

        jObjectDocument.Properties.Should().BeNull();
    }

    private static JObjectDocument ParseFile(string resourceFileName)
    {
        using Stream stream = EmbeddedResources.GetResourceStream(resourceFileName);
        return JObjectDocument.Parse(stream);
    }
}