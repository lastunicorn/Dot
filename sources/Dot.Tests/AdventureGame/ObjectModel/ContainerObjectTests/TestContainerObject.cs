using System.Collections;
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace Dot.Tests.AdventureGame.ObjectModel.ContainerObjectTests
{
    internal class TestContainerObject : ContainerObject
    {
        public override string Id { get; } = "test-container-object";

        public override string Name { get; } = "test container object";

        public override IEnumerable LookAt()
        {
            throw new System.NotImplementedException();
        }
    }
}