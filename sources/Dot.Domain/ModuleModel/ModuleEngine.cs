using System;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.Dot.Domain.ModuleModel
{
    public class ModuleEngine
    {
        private readonly List<IModule> modules = new List<IModule>();

        private IModule currentModule;
        private volatile bool closeWasRequested;
        private string requestedNextModuleId;

        public event EventHandler<ModuleRunExceptionEventArgs> ModuleRunException;

        public ModuleEngine()
        {
        }

        public ModuleEngine(IEnumerable<IModule> modules)
        {
            if (modules != null)
                this.modules.AddRange(modules);
        }

        public void AddModule(IModule module)
        {
            if (module == null) throw new ArgumentNullException(nameof(module));

            modules.Add(module);
        }

        public void Run()
        {
            closeWasRequested = false;

            IModule firstModule = modules.FirstOrDefault();

            if (firstModule == null)
                throw new Exception("There are no modules configured.");

            string nextModuleId = firstModule.Id;

            while (!closeWasRequested)
            {
                try
                {
                    currentModule = CalculateNextModule(nextModuleId);
                    nextModuleId = currentModule?.Run();

                    if (requestedNextModuleId != null)
                    {
                        nextModuleId = requestedNextModuleId;
                        requestedNextModuleId = null;
                    }
                }
                catch (Exception ex)
                {
                    ModuleRunExceptionEventArgs eventArgs = new ModuleRunExceptionEventArgs(ex);
                    OnModuleRunException(eventArgs);

                    nextModuleId = eventArgs.NextModule ?? modules.FirstOrDefault()?.Id;
                }
            }
        }

        private IModule CalculateNextModule(string nextModuleId)
        {
            IModule nextModule = modules.FirstOrDefault(x => x.Id == nextModuleId);

            if (nextModule == null)
                throw new Exception("Module not found.");

            return nextModule;
        }

        public void RequestToChangeModule(string moduleId)
        {
            requestedNextModuleId = moduleId;
            currentModule?.RequestExit();
        }

        public bool Close()
        {
            closeWasRequested = true;
            currentModule?.RequestExit();

            return true;
        }

        protected virtual void OnModuleRunException(ModuleRunExceptionEventArgs e)
        {
            ModuleRunException?.Invoke(this, e);
        }
    }
}