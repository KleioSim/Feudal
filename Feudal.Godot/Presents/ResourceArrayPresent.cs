using System.Linq;

namespace Feudal.Godot.Presents;

partial class ResourceArrayPresent : PresentControl<ResourceArrayView, ISessionModel>
{
    protected override void Initialize(ResourceArrayView view, ISessionModel model)
    {

    }

    protected override void Update(ResourceArrayView view, ISessionModel model)
    {
        var terrain = model.Session.Terrains[view.TerrainPos];

        view.Container.Refresh(terrain.Resources.Select(x => x.Id as object).ToHashSet());
    }
}
