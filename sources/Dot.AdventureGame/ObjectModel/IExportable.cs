using DustInTheWind.Dot.AdventureGame.ExportModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface IExportable
    {
        ExportNode Export();

        void Import(ExportNode exportNode);
    }
}