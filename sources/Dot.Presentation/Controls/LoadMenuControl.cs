using System;
using System.Collections.Generic;
using DustInTheWind.Dot.Application.UseCases.LoadGame;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls;

namespace DustInTheWind.Dot.Presentation.Controls
{
    internal class LoadMenuControl : SelectableMenu<GameSlotId>
    {
        private const int TotalSlotCount = 10;

        public void Populate(IEnumerable<GameSlotId> gameSlotIds)
        {
            IEnumerable<IMenuItem<GameSlotId>> menuItems = CreateMenuItems(gameSlotIds);
            AddRange(menuItems);
        }

        private static IEnumerable<IMenuItem<GameSlotId>> CreateMenuItems(IEnumerable<GameSlotId> gameSlotIds)
        {
            int count = 0;

            foreach (GameSlotId gameSlotId in gameSlotIds)
            {
                while (count < gameSlotId.Id - 1 && count < TotalSlotCount)
                {
                    count++;
                    yield return CreateEmptyMenuItem(count);
                }

                if (count == TotalSlotCount)
                    yield break;

                count++;
                yield return CreateMenuItem(gameSlotId);
            }

            while (count < TotalSlotCount)
            {
                count++;
                yield return CreateEmptyMenuItem(count);
            }

            yield return new SpaceMenuItem<GameSlotId>();

            yield return CreateCancelMenuItem();
        }

        private static IMenuItem<GameSlotId> CreateMenuItem(GameSlotId gameSlotId)
        {
            return new LabelMenuItem<GameSlotId>
            {
                Text = $"Saved Game {gameSlotId.Id}",
                Value = gameSlotId,
                HorizontalAlign = HorizontalAlign.Center
            };
        }

        private static IMenuItem<GameSlotId> CreateEmptyMenuItem(int id)
        {
            return new LabelMenuItem<GameSlotId>
            {
                Text = $"< Empty Slot {id} >",
                Value = new GameSlotId(id),
                HorizontalAlign = HorizontalAlign.Center,
                IsSelectable = false
            };
        }

        private static IMenuItem<GameSlotId> CreateCancelMenuItem()
        {
            return new LabelMenuItem<GameSlotId>
            {
                Text = "Cancel",
                Value = null,
                HorizontalAlign = HorizontalAlign.Center,
                Key = ConsoleKey.Escape
            };
        }
    }
}