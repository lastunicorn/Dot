namespace DustInTheWind.Dot.Presentation.ConsoleHelpers
{
    // ╔═════════╦═════════╗        201--205--203--205--187
    // ║         ║         ║         |         |         |
    // ║         ║         ║        186       186       186
    // ║         ║         ║         |         |         |
    // ╠═════════╬═════════╣        204--205--206--205--185
    // ║         ║         ║         |         |         |
    // ║         ║         ║        186       186       186
    // ║         ║         ║         |         |         |
    // ╚═════════╩═════════╝        200--205--202--205--188


    // ┌─────────┬─────────┐        218--196--194--196--191
    // │         │         │         |         |         |
    // │         │         │        179       179       179
    // │         │         │         |         |         |
    // ├─────────┼─────────┤        195--196--197--196--180
    // │         │         │         |         |         |
    // │         │         │        179       179       179
    // │         │         │         |         |         |
    // └─────────┴─────────┘        192--196--193--196--217


    // Ň═════════Đ═════════Ş        213--205--209--205--184
    // │         │         │         |         |         |
    // │         │         │        179       179       179
    // │         │         │         |         |         |
    // Ă═════════ě═════════Á        198--205--216--205--181
    // │         │         │         |         |         |
    // │         │         │        179       179       179
    // │         │         │         |         |         |
    // ď═════════¤═════════ż        212--205--207--205--190


    // Í─────────Ď─────────Ě        214--196--210--196--183
    // ║         ║         ║         |         |         |
    // ║         ║         ║        186       186       186
    // ║         ║         ║         |         |         |
    // ă─────────Î─────────Â        199--196--215--196--182
    // ║         ║         ║         |         |         |
    // ║         ║         ║        186       186       186
    // ║         ║         ║         |         |         |
    // Ë─────────đ─────────Ż        211--196--208--196--189

    public struct Border
    {
        public char TopLeft { get; set; }
        public char Top { get; set; }
        public char TopRight { get; set; }
        public char Left { get; set; }
        public char Right { get; set; }
        public char BottomLeft { get; set; }
        public char Bottom { get; set; }
        public char BottomRight { get; set; }

        public bool IsTopVisible { get; set; }
        public bool IsRightVisible { get; set; }
        public bool IsBottomVisible { get; set; }
        public bool IsLeftVisible { get; set; }

        public static Border CreateDoubleLineBorder(bool left, bool top, bool right, bool bottom)
        {
            return new Border
            {
                TopLeft = '╔',
                Top = '═',
                TopRight = '╗',

                Left = '║',
                Right = '║',

                BottomLeft = '╚',
                Bottom = '═',
                BottomRight = '╝',

                IsLeftVisible = left,
                IsRightVisible = right,
                IsTopVisible = top,
                IsBottomVisible = bottom
            };
        }

        public static Border CreateSingleLineBorder(bool left, bool top, bool right, bool bottom)
        {
            return new Border
            {
                TopLeft = '┌',
                Top = '─',
                TopRight = '┐',

                Left = '│',
                Right = '│',

                BottomLeft = '└',
                Bottom = '─',
                BottomRight = '┘',

                IsLeftVisible = left,
                IsRightVisible = right,
                IsTopVisible = top,
                IsBottomVisible = bottom
            };
        }
    }
}