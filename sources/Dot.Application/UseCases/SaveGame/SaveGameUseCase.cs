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

using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Ports.ConfigAccess;
using DustInTheWind.Dot.Ports.GameSavesAccess;
using DustInTheWind.Dot.Ports.UserAccess;
using MediatR;

namespace DustInTheWind.Dot.Application.UseCases.SaveGame;

public class SaveGameUseCase : IRequestHandler<SaveGameRequest>
{
    private readonly IGameSavingTerminal gameSavingTerminal;
    private readonly Game game;
    private readonly IGameSlotRepository gameSlotRepository;
    private readonly IGameSettings gameSettings;

    public SaveGameUseCase(IGameSavingTerminal gameSavingTerminal, Game game, IGameSlotRepository gameSlotRepository, IGameSettings gameSettings)
    {
        this.gameSavingTerminal = gameSavingTerminal ?? throw new ArgumentNullException(nameof(gameSavingTerminal));
        this.game = game ?? throw new ArgumentNullException(nameof(game));
        this.gameSlotRepository = gameSlotRepository ?? throw new ArgumentNullException(nameof(gameSlotRepository));
        this.gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
    }

    public Task Handle(SaveGameRequest request, CancellationToken cancellationToken)
    {
        if (!game.IsLoaded)
            throw new GameNotRunningException();

        List<GameSlot> gameSlots = RetrieveAvailableSlots();
        GameSlot selectedGameSlot = ChooseSlot(gameSlots);
        StoreCurrentGameInSlot(selectedGameSlot);
        UpdateSettings(selectedGameSlot);

        return Task.CompletedTask;
    }

    private List<GameSlot> RetrieveAvailableSlots()
    {
        return gameSlotRepository.GetAll().ToList();
    }

    private GameSlot ChooseSlot(List<GameSlot> gameSlots)
    {
        IEnumerable<GameSlotId> gameSlotIds = gameSlots.Select(x => new GameSlotId(x.Id));
        GameSlotId selectedGameSlotId = gameSavingTerminal.SelectGameSlot(gameSlotIds);

        if (selectedGameSlotId == null)
            throw new OperationCanceledException();

        return gameSlots.FirstOrDefault(x => x.Id == selectedGameSlotId.Id) ?? new GameSlot { Id = selectedGameSlotId. Id };
    }

    private void StoreCurrentGameInSlot(GameSlot gameSlot)
    {
        gameSlot.Data = game.Export().ToStorageData();
        gameSlotRepository.AddOrReplace(gameSlot);
    }

    private void UpdateSettings(GameSlot gameSlot)
    {
        gameSettings.LastSavedGame = gameSlot.Id;
    }
}