using System.Linq;

namespace Feudal.Godot.Presents;

partial class WorkingArrayPresent : PresentControl<WorkingArrayView, ISessionModel>
{
    protected override void Initialize(WorkingArrayView view, ISessionModel model)
    {

    }

    protected override void Update(WorkingArrayView view, ISessionModel model)
    {
        var workHood = model.Session.WorkHoods[view.WorkHoodId];
        view.CurrentWorking.Id = workHood.CurrentWorking.Id;

        view.OptionWorkings.Refresh(workHood.OptionWorkings.Select(x => x.Id as object).ToHashSet());
    }
}
