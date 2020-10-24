using System;
using DustInTheWind.Dot.Domain.ModuleModel;

namespace DustInTheWind.Dot.Application.UseCases
{
    public class ChangeModuleUseCase
    {
        private readonly ModuleEngine moduleEngine;

        public ChangeModuleUseCase(ModuleEngine moduleEngine)
        {
            this.moduleEngine = moduleEngine ?? throw new ArgumentNullException(nameof(moduleEngine));
        }

        public void Execute(ChangeModuleRequest request)
        {
            moduleEngine.RequestToChangeModule(request.ModuleId);
        }
    }
}