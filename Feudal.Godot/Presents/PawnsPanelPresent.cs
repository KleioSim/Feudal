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
        var positions = model.Session.Tasks.Values
            .Where(x => x.ClanId != null)
            .Select(x => model.Session.WorkHoods[x.WorkHoodId])
            .OfType<ITerrainWorkHood>()
            .Select(x => x.Position);

        view.WorkingPawns.Refresh(positions.Select(x=>x as object).ToHashSet());
    }
}
