using Feudal.Interfaces;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class ProgressWorkingPawnPresent : PresentControl<ProgressWorkingPawnView, ISessionModel>
{
    protected override void Initialize(ProgressWorkingPawnView view, ISessionModel model)
    {

    }

    protected override void Update(ProgressWorkingPawnView view, ISessionModel model)
    {
        var terrain = model.Session.Terrains[view.TerrainPosition];

        var task = model.Session.Tasks.Values.SingleOrDefault(x => x.WorkHood == terrain.WorkHood);
        view.ProgressValue = task.Percent;

        view.Label.Text = terrain.WorkHood.CurrentWorking.Name;

        view.Value.Text = (task.Step >= 0 ? "+" : "") + task.Step.ToString("0.0");
    }
}
