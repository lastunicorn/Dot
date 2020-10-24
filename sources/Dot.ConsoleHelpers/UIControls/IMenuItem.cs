using System;

namespace DustInTheWind.Dot.ConsoleHelpers.UIControls
{
    public interface IMenuItem<out T>
    {
        int Id { get; }
        string Text { get; set; }
        T Value { get; }
        bool IsVisible { get; }
        HorizontalAlign HorizontalAlign { get; set; }
        bool IsSelectable { get; }
        ConsoleKey? Key { get; set; }
        ICommand Command { get; set; }

        void Display(int x, int y, bool selected, HorizontalAlign itemsHorizontalAlign);
        bool BeforeSelect();
    }
}