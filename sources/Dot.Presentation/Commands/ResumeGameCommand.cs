using System;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.Application.LoadGame;
using DustInTheWind.Dot.Application.ResumeGame;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class ResumeGameCommand : ICommand
    {
        private readonly GameRepository gameRepository;
        private readonly IUseCaseFactory useCaseFactory;

        public event EventHandler CanExecuteChanges;

        public ResumeGameCommand(GameRepository gameRepository, IUseCaseFactory useCaseFactory)
        {
            this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public bool CanExecute()
        {
            IGame game = gameRepository.Get();
            return game != null;
        }

        public void Execute()
        {
            ResumeGameUseCase useCase = useCaseFactory.Create<ResumeGameUseCase>();
            useCase.Execute();
        }

        protected virtual void OnCanExecuteChanges()
        {
            CanExecuteChanges?.Invoke(this, EventArgs.Empty);
        }
    }
}