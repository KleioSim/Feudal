using Feudal.Godot.UICommands;
using Feudal.Interfaces;
using Godot;
using System.Collections.Generic;
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
            return Present.Model.Session.Tasks.Values.Any(x => x.Working.WorkHood == View.Id);
        }
        set
        {
            var session = Present.Model.Session as SessionMock;

            var workHood = View.Id as IWorkHood;

            var task = session.MockTasks.Values.SingleOrDefault(x => x.Working.WorkHood == workHood) as TaskMock;
            if (value)
            {
                if (task != null)
                {
                    return;
                }

                task = session.GenerateTask();
                task.Labor = session.GenerateClan().GenerateLabor();

                task.Working = workHood.OptionWorkings.FirstOrDefault();
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
            View.Id = workHood;

            for (int i = 0; i < 2; i++)
            {
                session.GenerateProgressWorking(workHood);
            }

            var task = session.GenerateTask();
            task.Labor = session.GenerateClan().GenerateLabor();

            task.Working = workHood.OptionWorkings.First();

            return new SessionModel() { Session = session };
        }
    }
}

class MockWorkHood : IWorkHood
{
    private static int count = 0;

    public string Id { get; }

    public IWorking CurrentWorking => throw new System.Exception();

    public IEnumerable<IWorking> OptionWorkings => MockOptionWorkings;

    public List<IWorking> MockOptionWorkings = new List<IWorking>();

    public MockWorkHood()
    {
        Id = $"WORK_HOOD{count++}";
    }
}

class MockTerrainWorkHood : MockWorkHood, ITerrainWorkHood
{
    public (int x, int y) Position { get; set; }
}