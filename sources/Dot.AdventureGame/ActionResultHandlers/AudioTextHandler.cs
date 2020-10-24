using System;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class AudioTextHandler : ResultHandlerBase<IAudioTextEnumerable>
    {
        private readonly IUserInterface userInterface;

        public AudioTextHandler(IUserInterface userInterface)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        }

        public override void Handle(IAudioTextEnumerable audioText)
        {
            userInterface.DisplayInfo(audioText);
        }
    }
}