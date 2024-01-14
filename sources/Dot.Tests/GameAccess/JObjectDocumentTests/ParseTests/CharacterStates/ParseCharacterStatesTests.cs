﻿// Dot
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

namespace Dot.Tests.GameAccess.JObjectDocumentTests.ParseTests.CharacterStates;

public class ParseCharacterStatesTests
{
    [Fact]
    public void HavingFileContainingOneCharacterStateInArray_WhenParsed_ThenCharacterStatesArrayContainsOnlyThatValue()
    {
        JObjectDocument jObjectDocument = ParseFile("character-states-01-object-with-one-character-state.json");

        string[] expected = { "in-bed" };
        jObjectDocument.CharacterStates.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void HavingFileContainingTwoCharacterStatesInArray_WhenParsed_ThenCharacterStatesArrayContainsBothValues()
    {
        JObjectDocument jObjectDocument = ParseFile("character-states-02-object-with-two-character-states.json");

        string[] expected = { "in-bed", "not-in-bed" };
        jObjectDocument.CharacterStates.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    [Fact]
    public void HavingFileContainingNoCharacterStatesInArray_WhenParsed_ThenCharacterStatesArrayIsEmpty()
    {
        JObjectDocument jObjectDocument = ParseFile("character-states-03-object-with-no-character-states-in-array.json");

        jObjectDocument.CharacterStates.Should().BeEmpty();
    }

    [Fact]
    public void HavingFileContainingNoCharacterStatesArray_WhenParsed_ThenCharacterStatesArrayIsNull()
    {
        JObjectDocument jObjectDocument = ParseFile("character-states-04-object-with-no-character-states-array.json");

        jObjectDocument.CharacterStates.Should().BeNull();
    }

    private static JObjectDocument ParseFile(string resourceFileName)
    {
        using Stream stream = EmbeddedResources.GetResourceStream(resourceFileName);
        return JObjectDocument.Parse(stream);
    }
}