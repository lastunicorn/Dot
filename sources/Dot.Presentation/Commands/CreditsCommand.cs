﻿using System;
using DustInTheWind.Dot.Application.UseCases;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class CreditsCommand : ICommand
    {
        private readonly IUseCaseFactory useCaseFactory;

        public event EventHandler CanExecuteChanges;

        public CreditsCommand(IUseCaseFactory useCaseFactory)
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            CreditsUseCase useCase = useCaseFactory.Create<CreditsUseCase>();
            useCase.Execute();
        }
    }
}