namespace DustInTheWind.Dot.Domain.ModuleModel
{
    public interface IModule
    {
        string Id { get; }

        string Run();

        void RequestExit();
    }
}