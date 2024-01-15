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

public class MainMenuAction : ActionBase
{
    private readonly ModuleEngine moduleEngine;

    public MainMenuAction(ModuleEngine moduleEngine)
        : base("menu", "m")
    {
        this.moduleEngine = moduleEngine ?? throw new ArgumentNullException(nameof(moduleEngine));
    }

    public override string Description => "Displays the main menu of the game.";

    public override List<string> Usage => new() { "<<:menu>>", "<<:m>>" };

    public override ActionType ActionType => ActionType.EnvironmentCommand;

    protected override List<Regex> CreateMatchers()
    {
        return new List<Regex>
        {
            new(@"^\s*(:menu|:m)\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
        };
    }

    protected override string[] ExtractParameters(Match match)
    {
        return Array.Empty<string>();
    }

    public override IEnumerable Execute(params object[] parameters)
    {
        moduleEngine.RequestToChangeModule("main-menu");

        yield break;
    }
}