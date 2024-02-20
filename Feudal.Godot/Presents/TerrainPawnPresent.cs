using Feudal.Interfaces;
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

        view.ResourcePawns.Refresh(!terrain.IsDiscoverd ? new HashSet<object>() : terrain.Resources.Select(x => x.Id as object).ToHashSet());

        var task = model.Session.Tasks.Values.SingleOrDefault(x => x.Working.WorkHood == terrain.WorkHood);
        if (task == null)
        {
            view.ProgressWorkingPawn.Visible = false;
            view.ProductWorkingPawn.Visible = false;
            return;
        }

        switch (task?.Working)
        {
            case IProgressWorking:
                view.ProgressWorkingPawn.Visible = true;
                view.ProductWorkingPawn.Visible = false;
                break;
            case IProductWorking:
                view.ProgressWorkingPawn.Visible = false;
                view.ProductWorkingPawn.Visible = true;
                break;
        }
    }
}