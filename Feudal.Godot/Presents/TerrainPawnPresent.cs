using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class TerrainPawnPresent : PresentControl<TerrainPawnView, ISessionModel>
{
    protected override void Initialize(TerrainPawnView view, ISessionModel model)
    {

    }

    protected override void Update(TerrainPawnView view, ISessionModel model)
    {
        var terrain = model.Session.Terrains[view.TerrainPosition];

        var task = model.Session.Tasks.Values.SingleOrDefault(x => x.WorkHood == terrain.WorkHood);
        view.WorkingPawn.Visible = task != null;

        view.ResourcePawns.Refresh(!terrain.IsDiscoverd ? new HashSet<object>() : terrain.Resources.Select(x => x.Id as object).ToHashSet());
    }
}