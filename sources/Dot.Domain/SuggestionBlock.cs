using System.Collections.Generic;

namespace DustInTheWind.Dot.Domain
{
    public class SuggestionBlock
    {
        private List<string> texts = new List<string>();

        public string Text
        {
            get => texts.Count > 0 ? texts[0] : null;
            set
            {
                if (texts.Count > 0)
                    texts[0] = value;
                else
                    texts.Add(value);
            }
        }

        public List<string> Texts
        {
            get => texts;
            set => texts = value ?? new List<string>();
        }
    }
}