using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.Domain
{
    public class GameSlot
    {
        public int Id { get; set; }

        public SaveData Data { get; set; }
    }
}