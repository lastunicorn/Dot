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

namespace Dot.Tests.GameAccess.JObjectDocumentTests.ParseTests.Id;

public class ParseIdTests
{
    [Fact]
    public void HavingFileContainingId_WhenParsed_ThenDocumentContainsId()
    {
        JObjectDocument jObjectDocument = ParseFile("id-01-object-with-id.json");
        jObjectDocument.Id.Should().Be("flower");
    }

    [Fact]
    public void HavingFileContainingNoId_WhenParsed_ThenDocumentContainsNullId()
    {
        JObjectDocument jObjectDocument = ParseFile("id-02-object-with-no-id.json");
        jObjectDocument.Id.Should().BeNull();
    }

    private static JObjectDocument ParseFile(string resourceFileName)
    {
        using Stream stream = EmbeddedResources.GetResourceStream(resourceFileName);
        return JObjectDocument.Parse(stream);
    }
}