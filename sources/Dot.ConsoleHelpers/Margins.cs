namespace DustInTheWind.Dot.Presentation.ConsoleHelpers
{
    public struct Margins
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public Margins(int value)
            : this()
        {
            Left = value;
            Top = value;
            Right = value;
            Bottom = value;
        }

        public Margins(int horizontalMargin, int verticalMargin)
            : this()
        {
            Left = horizontalMargin;
            Right = horizontalMargin;
            Top = verticalMargin;
            Bottom = verticalMargin;
        }

        public Margins(int left, int top, int right, int bottom)
            : this()
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }
}