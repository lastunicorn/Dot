using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.Application.UseCases.LoadGame;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Ports.GameSavesAccess;

namespace DustInTheWind.Dot.Application.UseCases.SaveGame;

public class SaveGameUseCase
{
    private readonly ISaveGameView saveGameView;
    private readonly GameRepository gameRepository;
    private readonly IGameSlotRepository gameSlotRepository;
    private readonly IGameSettings gameSettings;

    public SaveGameUseCase(ISaveGameView saveGameView, GameRepository gameRepository, IGameSlotRepository gameSlotRepository, IGameSettings gameSettings)
    {
        this.saveGameView = saveGameView ?? throw new ArgumentNullException(nameof(saveGameView));
        this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        this.gameSlotRepository = gameSlotRepository ?? throw new ArgumentNullException(nameof(gameSlotRepository));
        this.gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
    }

    public void Execute()
    {
        IGame game = gameRepository.Get();

        if (game == null)
            throw new Exception("There is no game to be saved.");

        List<GameSlot> gameSlots = gameSlotRepository.GetAll().ToList();
        GameSlotId gameSlotId = saveGameView.SelectGameSlot(gameSlots.Select(x => new GameSlotId(x.Id)));

        if (gameSlotId == null)
            throw new OperationCanceledException();

        GameSlot gameSlot = gameSlots.FirstOrDefault(x => x.Id == gameSlotId.Id);

        gameSlot.Data = game.Export().ToStorageData();
        gameSlotRepository.AddOrReplace(gameSlot);

        gameSettings.LastSavedGame = gameSlot.Id;
    }
}