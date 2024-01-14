using System;

namespace DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls
{
    public class LabelMenuItem<T> : IMenuItem<T>
    {
        protected int lastX = -1;
        protected int lastY = -1;
        protected int lastLength = -1;

        public int Id { get; set; }
        public string Text { get; set; }
        public T Value { get; set; }

        public int PaddingLeft { get; set; }
        public int PaddingRight { get; set; }
        public HorizontalAlign HorizontalAlign { get; set; }
        public bool IsSelectable { get; set; }
        public ConsoleKey? Key { get; set; }

        public bool IsVisible => Command?.CanExecute() ?? VisibilityProvider == null || VisibilityProvider();

        public Func<bool> VisibilityProvider { get; set; }

        public ICommand Command { get; set; }

        public LabelMenuItem()
            : this(null, default)
        {
        }

        public LabelMenuItem(string text, T value)
        {
            Text = text;
            Value = value;

            PaddingLeft = 1;
            PaddingRight = 1;

            HorizontalAlign = HorizontalAlign.Left;
            IsSelectable = true;
        }

        public void Display(int x, int y, bool selected, HorizontalAlign horizontalAlign = HorizontalAlign.Default)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            try
            {
                Console.ForegroundColor = selected ? initialBackgroundColor : initialForegroundColor;
                Console.BackgroundColor = selected ? initialForegroundColor : initialBackgroundColor;

                string line = Text;

                if (PaddingLeft > 0)
                    line = new string(' ', PaddingLeft) + line;

                if (PaddingRight > 0)
                    line = line + new string(' ', PaddingRight);

                HorizontalAlign calculatedHorizontalAlign = horizontalAlign == HorizontalAlign.Default
                    ? HorizontalAlign
                    : horizontalAlign;

                switch (calculatedHorizontalAlign)
                {
                    case HorizontalAlign.Default:
                    case HorizontalAlign.Left:
                        break;

                    case HorizontalAlign.Center:
                        x -= line.Length / 2;
                        break;

                    case HorizontalAlign.Right:
                        x -= line.Length;
                        break;
                }

                Console.SetCursorPosition(x, y);
                Console.Write(line);

                lastX = x;
                lastY = y;
                lastLength = line.Length;
            }
            finally
            {
                Console.ForegroundColor = initialForegroundColor;
                Console.BackgroundColor = initialBackgroundColor;
            }
        }

        public virtual bool BeforeSelect()
        {
            return true;
        }
    }
}