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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Dot.GameHosting;
using DustInTheWind.Dot.AdventureGame.ActionModel;

namespace DustInTheWind.Dot.Application.GameHostActions;

public class ExitAction : ActionBase
{
    private readonly IModuleHost moduleHost;

    public ExitAction(IModuleHost moduleHost)
        : base("exit", "quit", "x")
    {
        this.moduleHost = moduleHost ?? throw new ArgumentNullException(nameof(moduleHost));
    }

    public override string Description => "Exits the game.";

    public override List<string> Usage => new() { "<<:exit>>", "<<:quit>>", "<<:x>>" };

    public override ActionType ActionType => ActionType.EnvironmentCommand;

    protected override List<Regex> CreateMatchers()
    {
        return new List<Regex>
        {
            new(@"^\s*(:exit|:quit|:x)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
        };
    }

    protected override string[] ExtractParameters(Match match)
    {
        return Array.Empty<string>();
    }

    public override IEnumerable Execute(params object[] parameters)
    {
        moduleHost.Close();

        yield break;
    }
}