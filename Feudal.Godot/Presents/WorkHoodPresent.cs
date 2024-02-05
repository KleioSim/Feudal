using Feudal.Godot.UICommands;
using System;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class WorkHoodPresent : PresentControl<WorkHoodView, ISessionModel>
{
    protected override void Initialize(WorkHoodView view, ISessionModel model)
    {
        view.OccupyLabor += (string clanId) =>
        {
            SendCommand(new UICommand_OccupyLabor() { ClanId = clanId, WorkHoodId = view.Id });
        };

        view.CancelLaborButton.Pressed += () =>
        {
            var task = model.Session.Tasks.Values.SingleOrDefault(x => x.WorkHood.Id == view.Id);
            if (task == null)
            {
                throw new Exception($"WorkHood{view.Id}未关联Labor");
            }

            SendCommand(new UICommand_ReleaseLabor() { ClanId = task.Clan.Id, WorkHoodId = view.Id });
        };
    }


    protected override void Update(WorkHoodView view, ISessionModel model)
    {
        view.WorkingArray.WorkHoodId = view.Id;

        var task = model.Session.Tasks.Values.SingleOrDefault(x => x.WorkHood.Id == view.Id);
        if (task == null)
        {
            view.CurrentLabor.Visible = false;
            return;
        }

        view.CurrentLabor.Visible = true;
        view.ClanName.Text = task.Clan.Name;
    }
}
