using Feudal.Interfaces;
using System;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class WorkingItemPresent : PresentControl<WorkingItemView, ISessionModel>
{
    protected override void Initialize(WorkingItemView view, ISessionModel model)
    {

    }

    protected override void Update(WorkingItemView view, ISessionModel model)
    {
        var working = model.Session.Workings[view.Id];
        view.Button.Text = working.Name;

        switch (working)
        {
            case IProgressWorking progressWorking:
                {
                    view.ProgressPanel.Visible = true;
                    view.ProductPanel.Visible = false;

                    var workHood = model.Session.WorkHoods[view.WorkHoodId];
                    var task = model.Session.Tasks.Values.SingleOrDefault(x => x.WorkHoodId == (string)view.WorkHoodId);
                    if (task != null && progressWorking == workHood.CurrentWorking)
                    {
                        view.ProgressBar.Value = task.Percent;
                    }
                }
                break;
            default:
                throw new Exception();
        }
    }
}
