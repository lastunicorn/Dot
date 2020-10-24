using System;

namespace DustInTheWind.Dot.ConsoleHelpers.UIControls
{
    public class SpaceMenuItem<T> : IMenuItem<T>
    {
        public int Id => -1;

        public string Text
        {
            get => string.Empty;
            set { }
        }

        public T Value => default;

        public bool IsVisible { get; set; }

        public HorizontalAlign HorizontalAlign { get; set; }

        public SpaceMenuItem()
        {
            IsVisible = true;
        }

        public bool IsSelectable => false;

        public ConsoleKey? Key
        {
            get => null;
            set { }
        }

        public ICommand Command { get; set; }

        public void Display(int x, int y, bool selected, HorizontalAlign itemsHorizontalAlign)
        {
        }

        public virtual bool BeforeSelect()
        {
            return true;
        }
    }
}