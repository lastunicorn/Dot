using System;
using System.Collections.Generic;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.Presentation.Controls
{
    internal class LoadMenuControl : SelectableMenu<GameSlot>
    {
        private const int TotalSlotCount = 10;

        public void Populate(IEnumerable<GameSlot> gameSlots)
        {
            IEnumerable<IMenuItem<GameSlot>> menuItems = CreateMenuItems(gameSlots);
            AddRange(menuItems);
        }

        private static IEnumerable<IMenuItem<GameSlot>> CreateMenuItems(IEnumerable<GameSlot> gameSlots)
        {
            int count = 0;

            foreach (GameSlot gameSlot in gameSlots)
            {
                while (count < gameSlot.Id - 1 && count < TotalSlotCount)
                {
                    count++;
                    yield return CreateEmptyMenuItem(count);
                }

                if (count == TotalSlotCount)
                    yield break;

                count++;
                yield return CreateMenuItem(gameSlot);
            }

            while (count < TotalSlotCount)
            {
                count++;
                yield return CreateEmptyMenuItem(count);
            }

            yield return new SpaceMenuItem<GameSlot>();

            yield return CreateCancelMenuItem();
        }

        private static IMenuItem<GameSlot> CreateMenuItem(GameSlot gameSlot)
        {
            return new LabelMenuItem<GameSlot>
            {
                Text = string.Format("Saved Game {0}", gameSlot.Id),
                Value = gameSlot,
                HorizontalAlign = HorizontalAlign.Center
            };
        }

        private static IMenuItem<GameSlot> CreateEmptyMenuItem(int id)
        {
            return new LabelMenuItem<GameSlot>
            {
                Text = $"< Empty Slot {id} >",
                Value = new GameSlot
                {
                    Id = id
                },
                HorizontalAlign = HorizontalAlign.Center,
                IsSelectable = false
            };
        }

        private static IMenuItem<GameSlot> CreateCancelMenuItem()
        {
            return new LabelMenuItem<GameSlot>
            {
                Text = "Cancel",
                Value = null,
                HorizontalAlign = HorizontalAlign.Center,
                Key = ConsoleKey.Escape
            };
        }
    }
}