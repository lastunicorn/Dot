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
using Dot.GameAccess;
using FluentAssertions;
using Xunit;

namespace Dot.Tests.GameAccess.JObjectDocumentTests.ParseTests.States;

public class ParseStatesTests
{
    [Fact]
    public void HavingFileContainingOneStateInArray_WhenParsed_ThenStatesArrayContainsOnlyThatValue()
    {
        JObjectDocument jObjectDocument = ParseFile("states-01-object-with-one-state.json");

        string[] expected = { "opened" };
        jObjectDocument.States.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void HavingFileContainingTwoStatesInArray_WhenParsed_ThenStatesArrayContainsBothValues()
    {
        JObjectDocument jObjectDocument = ParseFile("states-02-object-with-two-states.json");

        string[] expected = { "opened", "closed" };
        jObjectDocument.States.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    [Fact]
    public void HavingFileContainingNoStatesInArray_WhenParsed_ThenStatesArrayIsEmpty()
    {
        JObjectDocument jObjectDocument = ParseFile("states-03-object-with-no-states-in-array.json");

        jObjectDocument.States.Should().BeEmpty();
    }

    [Fact]
    public void HavingFileContainingNoStatesArray_WhenParsed_ThenStatesArrayIsNull()
    {
        JObjectDocument jObjectDocument = ParseFile("states-04-object-with-no-states-array.json");

        jObjectDocument.States.Should().BeNull();
    }

    private static JObjectDocument ParseFile(string resourceFileName)
    {
        using Stream stream = EmbeddedResources.GetResourceStream(resourceFileName);
        return JObjectDocument.Parse(stream);
    }
}