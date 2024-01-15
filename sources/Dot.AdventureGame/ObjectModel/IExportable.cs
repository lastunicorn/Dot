using DustInTheWind.Dot.Domain.GameModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface IExportable
    {
        ExportNode Export();

        void Import(ExportNode exportNode);
    }
}