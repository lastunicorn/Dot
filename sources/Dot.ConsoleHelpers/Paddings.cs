namespace DustInTheWind.Dot.ConsoleHelpers
{
    public struct Paddings
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public Paddings(int value)
            : this()
        {
            Left = value;
            Top = value;
            Right = value;
            Bottom = value;
        }

        public Paddings(int horizontalMargin, int verticalMargin)
            : this()
        {
            Left = horizontalMargin;
            Top = verticalMargin;
            Right = horizontalMargin;
            Bottom = verticalMargin;
        }

        public Paddings(int left, int top, int right, int bottom)
            : this()
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }
}