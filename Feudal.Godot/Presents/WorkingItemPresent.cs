using Feudal.Interfaces;
using System;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class WorkingItemPresent : PresentControl<WorkingItemView, ISessionModel>
{
    protected override void Initialize(WorkingItemView view, ISessionModel model)
    {
        view.TooltipTrigger.funcGetToolTipString = () => GetWorkingEffectDescString(view, model);
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

                    var step = progressWorking.GetStep(workHood);
                    view.Step.Text = (step >= 0 ? "+" : "") + step.ToString("0.0");
                }
                break;
            default:
                throw new Exception();
        }
    }

    private string GetWorkingEffectDescString(WorkingItemView view, ISessionModel model)
    {
        var working = model.Session.Workings[view.Id] as IProgressWorking;
        var workHood = model.Session.WorkHoods[view.WorkHoodId];

        var step = working.GetStep(workHood);
        return step.ToString();
    }
}
