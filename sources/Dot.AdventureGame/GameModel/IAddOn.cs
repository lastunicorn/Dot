using DustInTheWind.Dot.Domain.GameModel;

namespace DustInTheWind.Dot.AdventureGame.GameModel
{
    public interface IAddOn
    {
        string Id { get; }

        Game Game { get; set; }

        void Start();

        void Stop();

        ExportNode Export();

        void Import(ExportNode exportNode);
    }
}