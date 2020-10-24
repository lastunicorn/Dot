using System;
using DustInTheWind.Dot.Application.SaveGame;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class SaveCommand : ICommand
    {
        private readonly IUseCaseFactory useCaseFactory;
        private readonly GameRepository gameRepository;

        public event EventHandler CanExecuteChanges;

        public SaveCommand(IUseCaseFactory useCaseFactory, GameRepository gameRepository)
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
            this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }

        public bool CanExecute()
        {
            IGameBase game = gameRepository.Get();
            return game != null;
        }

        public void Execute()
        {
            SaveGameUseCase useCase = useCaseFactory.Create<SaveGameUseCase>();
            useCase.Execute();
        }

        protected virtual void OnCanExecuteChanges()
        {
            CanExecuteChanges?.Invoke(this, EventArgs.Empty);
        }
    }
}