namespace DustInTheWind.Dot.Application.UseCases.Credits
{
    public class CreditsUseCase
    {
        public Domain.Credits Execute()
        {
            return new Domain.Credits
            {
                Scenarist = "Alexandru Iuga",
                Programmer = "Alexandru Iuga"
            };
        }
    }
}