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

namespace DustInTheWind.Dot.GameHosting;

public class ModuleEngine
{
    private readonly ModuleCollection modules = new();

    private IModule currentModule;
    private IModule defaultModule;
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

    public void SetDefaultModule(string moduleId)
    {
        defaultModule = GetModuleOrThrow(moduleId);
    }

    public void Run()
    {
        closeWasRequested = false;
        string nextModuleId = GetDefaultModuleId();

        while (!closeWasRequested)
        {
            try
            {
                currentModule = GetModuleOrThrow(nextModuleId);
                nextModuleId = currentModule?.Run();

                if (requestedNextModuleId != null)
                {
                    nextModuleId = requestedNextModuleId;
                    requestedNextModuleId = null;
                }
            }
            catch (Exception ex)
            {
                ModuleRunExceptionEventArgs eventArgs = new(ex);
                OnModuleRunException(eventArgs);

                nextModuleId = eventArgs.NextModule ?? GetDefaultModuleId();
            }
        }
    }

    private string GetDefaultModuleId()
    {
        if (defaultModule != null)
            return defaultModule.Id;

        IModule firstModule = modules.FirstOrDefault();

        return firstModule == null
            ? throw new NoModulesException()
            : firstModule.Id;
    }

    private IModule GetModuleOrThrow(string moduleId)
    {
        return modules.GetById(moduleId) ?? throw new ModuleNotFoundException(moduleId);
    }

    public void RequestToChangeModule(string moduleId)
    {
        requestedNextModuleId = moduleId;
        currentModule?.RequestExit();
    }

    public void RequestToClose()
    {
        closeWasRequested = true;
        currentModule?.RequestExit();
    }

    protected virtual void OnModuleRunException(ModuleRunExceptionEventArgs e)
    {
        ModuleRunException?.Invoke(this, e);
    }
}