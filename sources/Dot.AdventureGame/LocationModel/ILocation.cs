using System;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.AdventureGame.LocationModel
{
    public interface ILocation : IObject, IObjectContainer
    {
        string[] AdditionalNames { get; }

        AudioText ResumeDescription { get; }

        event EventHandler Entered;
        event EventHandler Exiting;

        void InitializeNew();

        bool HasName(string name);

        void Enter();

        void Exit();
    }
}