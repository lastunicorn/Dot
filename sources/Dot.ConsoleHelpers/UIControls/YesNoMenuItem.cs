using System;

namespace DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls
{
    public class YesNoMenuItem<T> : LabelMenuItem<T>
    {
        public string QuestionText { get; set; }

        public override bool BeforeSelect()
        {
            Console.SetCursorPosition(lastX + lastLength + 1, lastY);
            string message = QuestionText + " [Y/n]";
            Console.Write(message);
            ConsoleKeyInfo key = Console.ReadKey(true);

            Console.SetCursorPosition(lastX + lastLength + 1, lastY);
            Console.Write(new string(' ', message.Length));

            bool allow = key.Key == ConsoleKey.Y || key.Key == ConsoleKey.Enter;
            return allow && base.BeforeSelect();
        }
    }
}