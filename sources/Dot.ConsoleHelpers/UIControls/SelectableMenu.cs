using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.ConsoleUtil;

namespace DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls
{
    public class SelectableMenu<T> : List<IMenuItem<T>>
    {
        private int screenWidth;

        private List<IMenuItem<T>> visibleMenuItems;
        private int currentIndex = -1;

        private int rowAfterMenu;

        public int SelectedIndex { get; private set; }

        public HorizontalAlign ItemsHorizontalAlign { get; set; }

        public IMenuItem<T> SelectedItem { get; private set; }

        public event EventHandler BeforeDisplay;
        public event EventHandler AfterClose;

        public T Display()
        {
            OnBeforeDisplay();

            screenWidth = Console.BufferWidth;

            return RunWithoutCursor(() =>
            {
                try
                {
                    visibleMenuItems = this
                        .Where(x => x != null && x.IsVisible)
                        .ToList();

                    List<IMenuItem<T>> selectableItems = visibleMenuItems
                        .Where(x => !(x is SpaceMenuItem<string>))
                        .ToList();

                    if (selectableItems.Count == 0)
                        throw new Exception("There are no menu items to be displayed.");

                    DisplayMenuItems();
                    return ReadUserSelection();
                }
                finally
                {
                    DrawItem(currentIndex, false);
                    Console.SetCursorPosition(0, rowAfterMenu);

                    OnAfterClose();

                    SelectedItem?.Command?.Execute();
                }
            });
        }

        private static TReturn RunWithoutCursor<TReturn>(Func<TReturn> action)
        {
            bool initialCursorVisible = true;
            try
            {
                initialCursorVisible = Console.CursorVisible;
            }
            catch { }

            Console.CursorVisible = false;

            try
            {
                return action();
            }
            finally
            {
                Console.CursorVisible = initialCursorVisible;
            }
        }

        public T Resume()
        {
            bool initialCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;

            try
            {
                DrawItem(currentIndex, true);
                return ReadUserSelection();
            }
            finally
            {
                DrawItem(currentIndex, false);

                Console.SetCursorPosition(0, rowAfterMenu);
                Console.CursorVisible = initialCursorVisible;
            }
        }

        public void Refresh()
        {
            bool initialCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;

            try
            {
                DrawItem(currentIndex, true);
            }
            finally
            {
                DrawItem(currentIndex, false);

                Console.SetCursorPosition(0, rowAfterMenu);
                Console.CursorVisible = initialCursorVisible;
            }
        }

        private void DisplayMenuItems()
        {
            for (int i = 0; i < visibleMenuItems.Count; i++)
                Console.WriteLine();

            rowAfterMenu = Console.CursorTop;

            bool itemWasSelected = false;
            currentIndex = -1;

            for (int i = 0; i < visibleMenuItems.Count; i++)
            {
                bool isSelect = !itemWasSelected && !(visibleMenuItems[i] is SpaceMenuItem<string>);

                DrawItem(i, isSelect);

                if (isSelect)
                {
                    itemWasSelected = true;
                    currentIndex = i;
                }
            }
        }

        private T ReadUserSelection()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = CustomConsole.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        MoveCursorUp();
                        break;

                    case ConsoleKey.DownArrow:
                        MoveCursorDown();
                        break;

                    case ConsoleKey.Enter:
                        bool success = ExecuteSelectedItem();
                        if (success)
                            return SelectedItem.Value;
                        break;

                    default:
                        {
                            IMenuItem<T> selectedItem = visibleMenuItems.FirstOrDefault(x => x.Key != null && x.Key == keyInfo.Key);

                            if (selectedItem != null)
                            {
                                bool allow = selectedItem.IsSelectable && selectedItem.BeforeSelect();
                                if (allow)
                                {
                                    SelectedIndex = currentIndex - visibleMenuItems.Take(currentIndex).OfType<SpaceMenuItem<T>>().Count();
                                    SelectedItem = selectedItem;
                                    return selectedItem.Value;
                                }
                            }
                        }
                        break;
                }
            }
        }

        private void MoveCursorUp()
        {
            int previousIndex = GetPreviousItemIndex();
            if (previousIndex == currentIndex)
                return;

            DrawItem(currentIndex, false);
            currentIndex = GetPreviousItemIndex();
            DrawItem(currentIndex, true);
        }

        private int GetPreviousItemIndex()
        {
            int startIndex = currentIndex == -1
                ? visibleMenuItems.Count
                : currentIndex - 1;

            for (int i = startIndex; i >= 0; i--)
            {
                if (visibleMenuItems[i] is SpaceMenuItem<T>)
                    continue;

                return i;
            }

            return currentIndex;
        }

        private void MoveCursorDown()
        {
            int nextIndex = GetNextItemIndex();
            if (nextIndex == currentIndex)
                return;

            DrawItem(currentIndex, false);
            currentIndex = nextIndex;
            DrawItem(currentIndex, true);
        }

        private int GetNextItemIndex()
        {
            int startIndex = currentIndex == -1
                ? 0
                : currentIndex + 1;

            for (int i = startIndex; i < visibleMenuItems.Count; i++)
            {
                if (visibleMenuItems[i] is SpaceMenuItem<T>)
                    continue;

                return i;
            }

            return currentIndex;
        }

        private bool ExecuteSelectedItem()
        {
            IMenuItem<T> selectedItem = visibleMenuItems[currentIndex];

            bool allowToExecute = selectedItem.IsSelectable && selectedItem.BeforeSelect();
            if (allowToExecute)
            {
                SelectedIndex = currentIndex - visibleMenuItems.Take(currentIndex).OfType<SpaceMenuItem<T>>().Count();
                SelectedItem = selectedItem;
            }

            return allowToExecute;
        }

        private void DrawItem(int menuIndex, bool selected)
        {
            if (menuIndex == -1)
                return;

            IMenuItem<T> menuItem = visibleMenuItems[menuIndex];

            int x = screenWidth / 2 - 2;
            int y = rowAfterMenu - visibleMenuItems.Count + menuIndex;

            Console.SetCursorPosition(0, y);
            Console.Write(new string(' ', Console.BufferWidth - 1));

            menuItem.Display(x, y, selected, ItemsHorizontalAlign);
        }

        protected virtual void OnBeforeDisplay()
        {
            BeforeDisplay?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnAfterClose()
        {
            AfterClose?.Invoke(this, EventArgs.Empty);
        }
    }
}