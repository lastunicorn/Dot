using System;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public class ObjectAddingEventArgs : EventArgs
    {
        public IObject Object { get; }

        public ObjectAddingEventArgs(IObject @object)
        {
            Object = @object;
        }
    }
}