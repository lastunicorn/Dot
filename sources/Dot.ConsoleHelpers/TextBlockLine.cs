using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.Dot.ConsoleHelpers
{
    public class TextBlockLine : List<Chip>
    {
        public string Text
        {
            get { return string.Join("", this.Select(x => x.Text)); }
        }

        public bool IsFirst { get; set; }

        public int TextLength
        {
            get { return this.Sum(x => x.Text?.Length ?? 0); }
        }

        public TextBlockLine(bool isFirst = false)
        {
            IsFirst = isFirst;
        }

        public void Add(string text, ChipType chipType = ChipType.Text)
        {
            Add(new Chip
            {
                Text = text,
                Type = chipType
            });
        }

        public void TrimEnd()
        {
            if (Count == 0)
                return;

            Chip lastChip = this[Count - 1];

            this[Count - 1] = new Chip
            {
                Text = lastChip.Text.TrimEnd(),
                Type = lastChip.Type
            };
        }
    }
}