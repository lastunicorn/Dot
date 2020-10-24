using System;
using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class StoryBlockHandler : ResultHandlerBase<StoryBlock>
    {
        private readonly IUserInterface userInterface;

        public StoryBlockHandler(IUserInterface userInterface)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        }

        public override void Handle(StoryBlock storyBlock)
        {
            if (storyBlock.AsciiPath != null)
                userInterface.DisplayAsciiArt(storyBlock.AsciiPath, 2);

            userInterface.DisplayStoryTeller(storyBlock.AudioTexts, storyBlock.Title);
        }
    }
}