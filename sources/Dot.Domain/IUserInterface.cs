using System;
using System.Collections.Generic;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Domain
{
    public interface IUserInterface
    {
        void Display(List<Tuple<string, string, string>> list, int minColumn1Width, int topMargin, int bottomMargin);
        void DisplayInfo(string text);
        void DisplayInfo(IEnumerable<string> texts);
        void DisplayInfo(IAudioText audioTexts);
        void DisplayStoryTeller(IAudioText audioTexts, string title = null);
        void DisplaySuggestion(IEnumerable<string> texts, int wordWrapPadding = 0);
        void DisplaySuggestion(string text);
        void DisplaySuggestion(SuggestionBlock suggestionBlock);
        void PlayBackgroundSound(string audioFileName, Action action);
        T PlayBackgroundSound<T>(string audioFileName, Func<T> action);
        void DisplayAsciiArt(string id, int marginTop = 0, int marginBottom = 0);
        void DisplayInfoBlock(IEnumerable<string> texts);
        string Question(string text);
    }
}