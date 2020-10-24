using System.Collections;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public static class ReceiverExtensions
    {
        public static StoryBlock CreateReceiveStory(this IReceiver receiverObject, IObject receivedObject, IAudioTextEnumerable audioTexts)
        {
            return new StoryBlock
            {
                Title = "Use {{" + receivedObject.Name + "}} with {{" + receiverObject.Name + "}}",
                AudioTexts = audioTexts
            };
        }

        public static IEnumerable ReceiveWhenCurrentObjectIsNotYours(this IReceiver receiverObject, IObject receivedObject)
        {
            yield return CreateReceiveStory(receiverObject, receivedObject, new AudioText
            {
                Text = "You must acquire the object first and then use it.",
                AudioFileName = "receive-notyours.mp3"
            });
        }

        public static IEnumerable ReceiveObjectNotYours(this IReceiver receiverObject, IObject receivedObject)
        {
            yield return CreateReceiveStory(receiverObject, receivedObject, new AudioText
            {
                Text = "You must acquire the object first and then use it.",
                AudioFileName = "receive-notyours.mp3"
            });
        }

        public static IEnumerable ReceiveUnknownObject(this IReceiver receiverObject, IObject receivedObject)
        {
            yield return CreateReceiveStory(receiverObject, receivedObject, new ReceiveUnknownObjectAudioText());
        }
    }
}