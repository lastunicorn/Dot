using DustInTheWind.Dot.AdventureGame.ActionModel;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace DustInTheWind.Dot.AdventureGame.ActionResultHandlers
{
    public class DestroyObjectsResultHandler : ResultHandlerBase<DestroyObjectsResult>
    {
        public override void Handle(DestroyObjectsResult destroyObjectsResult)
        {
            if (destroyObjectsResult.Objects != null)
            {
                foreach (IObject obj in destroyObjectsResult.Objects)
                    obj.Parent.RemoveObject(obj);
            }
        }
    }
}