using System;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public class ObjectAddedEventArgs : EventArgs
    {
        public IObject Object { get; }

        public ObjectAddedEventArgs(IObject @object)
        {
            Object = @object;
        }
    }
}