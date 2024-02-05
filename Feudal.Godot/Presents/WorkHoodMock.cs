using Feudal.Godot.UICommands;
using Godot;
using System.Linq;

namespace Feudal.Godot.Presents;

[Tool]
partial class WorkHoodMock : MockControl<WorkHoodView, ISessionModel>
{
    [Export]
    public bool IsOccupyClan
    {
        get
        {
            return Present.Model.Session.Tasks.Values.Any(x => x.WorkHood.Id == View.Id);
        }
        set
        {
            var session = Present.Model.Session as SessionMock;

            var task = session.MockTasks.Values.SingleOrDefault(x => x.WorkHood.Id == View.Id) as TaskMock;
            if (value)
            {
                if (task != null)
                {
                    return;
                }

                task = new TaskMock();
                task.Clan = session.Clans.Values.Last();
                task.WorkHood = session.WorkHoods[View.Id];

                session.MockTasks.Add(task.Id, task);
            }
            else
            {
                if (task == null)
                {
                    return;
                }

                session.MockTasks.Remove(task.Id);
            }

            Present.SendCommand(new UICommand_Refresh());
        }
    }


    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            var workHood = session.GenerateWorkHood();
            View.Id = workHood.Id;

            for (int i = 0; i < 2; i++)
            {
                session.GenerateProgressWorking(workHood);
            }

            var task = session.GenerateTask();
            task.Clan = session.GenerateClan();
            task.WorkHood = workHood;

            return new SessionModel() { Session = session };
        }
    }
}