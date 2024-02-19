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

        var working = terrain.WorkHood.CurrentWorking as IProgressWorking;
        view.ProgressValue = working.Percent;
        view.Label.Text = working.Name;

        var step = working.GetEffectValue().Value;
        view.Value.Text = (step >= 0 ? "+" : "") + step.ToString("0.0");
    }
}
