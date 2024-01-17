using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.Application.UseCases.LoadGame;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Ports.GameSavesAccess;
using DustInTheWind.Dot.Ports.PresentationAccess;

namespace DustInTheWind.Dot.Application.UseCases.SaveGame;

public class SaveGameUseCase
{
    private readonly ISaveGameView saveGameView;
    private readonly Game game;
    private readonly IGameSlotRepository gameSlotRepository;
    private readonly IGameSettings gameSettings;

    public SaveGameUseCase(ISaveGameView saveGameView, Game game, IGameSlotRepository gameSlotRepository, IGameSettings gameSettings)
    {
        this.saveGameView = saveGameView ?? throw new ArgumentNullException(nameof(saveGameView));
        this.game = game ?? throw new ArgumentNullException(nameof(game));
        this.gameSlotRepository = gameSlotRepository ?? throw new ArgumentNullException(nameof(gameSlotRepository));
        this.gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
    }

    public void Execute()
    {
        if (!game.IsLoaded)
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