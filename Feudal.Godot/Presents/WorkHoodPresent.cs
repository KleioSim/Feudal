using Feudal.Commands;
using Feudal.Godot.UICommands;
using Feudal.Interfaces;
using System;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class WorkHoodPresent : PresentControl<WorkHoodView, ISessionModel>
{
    protected override void Initialize(WorkHoodView view, ISessionModel model)
    {
        view.OccupyLabor += (string clanId) =>
        {
            var working = view.CurrentWorking.Id as IWorking;

            SendCommand(new Command_OccupyLabor() { ClanId = clanId, WorkingId = working.Id });
        };

        view.CancelLaborButton.Pressed += () =>
        {
            var task = model.Session.Tasks.Values.SingleOrDefault(x => x.Working.WorkHood == view.Id);
            if (task == null)
            {
                throw new Exception($"WorkHood{view.Id}未关联Labor");
            }

            SendCommand(new Command_ReleaseLabor() { ClanId = task.Labor.Id, WorkingId = task.Working.Id });
        };

        view.SwitchWorking += (workingItem) =>
        {
            var working = workingItem.Id as IWorking;

            SendCommand(new Command_SwitchCurrentWorking() { WorkingId = working.Id });
        };
    }


    protected override void Update(WorkHoodView view, ISessionModel model)
    {
        var workHood = model.Session.WorkHoods[view.Id];
        view.CurrentWorking.Id ??= workHood.OptionWorkings.FirstOrDefault();

        view.OptionWorkings.Refresh(workHood.OptionWorkings
            .Where(x => x != view.CurrentWorking.Id)
            .Select(x => x as object).ToHashSet());

        var task = model.Session.Tasks.Values.SingleOrDefault(x => x.Working.WorkHood == view.Id);
        if (task == null)
        {
            view.CurrentLabor.Visible = false;
            return;
        }

        view.CurrentLabor.Visible = true;
        view.ClanName.Text = task.Labor.From.Name;
    }
}
