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
using System.Linq;
using System.Text.RegularExpressions;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.Application.UseCases.NewGame;

namespace DustInTheWind.Dot.Presentation.GameHostActions;

public class NewGameAction : ActionBase
{
    private readonly IUseCaseFactory useCaseFactory;

    public NewGameAction(IUseCaseFactory useCaseFactory)
        : base("new")
    {
        this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
    }

    public override string Description => "Starts a new game.";

    public override List<string> Usage => new() { "<<:new>>" };

    public override ActionType ActionType => ActionType.EnvironmentCommand;

    protected override List<Regex> CreateMatchers()
    {
        return new List<Regex>
        {
            new(@"^\s*:new\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline)
        };
    }

    protected override string[] ExtractParameters(Match match)
    {
        return Array.Empty<string>();
    }

    public override IEnumerable Execute(params object[] parameters)
    {
        CreateNewGameUseCase useCase = useCaseFactory.Create<CreateNewGameUseCase>();
        useCase.Execute();

        return Enumerable.Empty<object>();
    }
}