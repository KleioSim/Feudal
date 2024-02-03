using Feudal.Interfaces;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class PawnsPanelPresent : PresentControl<PawnsPanelView, ISessionModel>
{
    protected override void Initialize(PawnsPanelView view, ISessionModel model)
    {

    }

    protected override void Update(PawnsPanelView view, ISessionModel model)
    {
        var resourcePositions = model.Session.Terrains.Values.Where(x => x.IsDiscoverd && x.Resources.Any())
                        .Select(x => x.Position);

        var workingPositions = model.Session.Tasks.Values
            .Select(x => model.Session.WorkHoods[x.WorkHoodId])
            .OfType<ITerrainWorkHood>()
            .Select(x => x.Position);

        view.TerrainPawns.Refresh(resourcePositions.Concat(workingPositions).Select(x=>x as object).ToHashSet());
    }
}
