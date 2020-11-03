using System;
using DustInTheWind.Dot.Domain.ModuleModel;

namespace DustInTheWind.Dot.Application.UseCases.ResumeGame
{
    public class ResumeGameUseCase
    {
        private readonly ModuleEngine moduleEngine;

        public ResumeGameUseCase(ModuleEngine moduleEngine)
        {
            this.moduleEngine = moduleEngine ?? throw new ArgumentNullException(nameof(moduleEngine));
        }

        public void Execute()
        {
            moduleEngine.RequestToChangeModule("game");
        }
    }
}