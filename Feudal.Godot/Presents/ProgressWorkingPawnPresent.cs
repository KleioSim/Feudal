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
        var task = model.Session.Tasks.Values.Single(x =>
        {
            if (x.Working.WorkHood is ITerrainWorkHood terrainWorkHood)
            {
                return terrainWorkHood.Position == view.TerrainPosition;
            }

            return false;
        });

        var working = task.Working as IProgressWorking;
        view.ProgressValue = working.Percent;
        view.Label.Text = working.Name;

        var step = working.GetEffectValue().Value;
        view.Value.Text = (step >= 0 ? "+" : "") + step.ToString("0.0");
    }
}
