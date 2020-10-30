using System.Collections;
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace Dot.Tests.AdventureGame.ObjectModel.ContainerObjectTests
{
    internal class TestObject : ObjectBase
    {
        public override string Id { get; } = "test-object";

        public override string Name { get; } = "test object";

        public override IEnumerable LookAt()
        {
            throw new System.NotImplementedException();
        }
    }
}