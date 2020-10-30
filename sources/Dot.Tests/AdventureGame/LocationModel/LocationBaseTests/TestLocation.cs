using System;
using System.Collections;
using System.Collections.Generic;
using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace Dot.Tests.AdventureGame.LocationModel.LocationBaseTests
{
    internal class TestLocation : LocationBase
    {
        public override string Id { get; } = "test-location";

        public override string Name { get; } = "test location";

        public new HashSet<IObject> Children => base.Children;

        public new AddOnCollection AddOns => base.AddOns;

        public override IEnumerable LookAt()
        {
            throw new NotImplementedException();
        }

        public override AudioText ResumeDescription { get; }

        public override void InitializeNew()
        {
            TestObject testObject = new TestObject();
            base.Children.Add(testObject);

            TestAddOn testAddOn = new TestAddOn();
            base.AddOns.Add(testAddOn);
        }
    }
}