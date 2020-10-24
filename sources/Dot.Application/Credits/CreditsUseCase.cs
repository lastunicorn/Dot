using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.Application.UseCases
{
    public class CreditsUseCase
    {
        public Credits Execute()
        {
            return new Credits
            {
                Scenarist = "Alexandru Iuga",
                Programmer = "Alexandru Iuga"
            };
        }
    }
}