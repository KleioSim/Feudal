using Feudal.Godot.UICommands;
using Feudal.Interfaces;
using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feudal.Godot.Presents;

[Tool]
partial class TaskArrayMock : MockControl<TaskArrayView, ISessionModel>
{
    [Export]
    public int taskCount
    {
        get
        {
            return Present.Model.Session.Tasks.Count();
        }
        set
        {
            var session = Present.Model.Session as SessionMock;

            while (value > session.MockTasks.Count())
            {
                var task = session.GenerateTask();

                var workHood = session.GenerateWorkHood();
                task.Working = session.GenerateProgressWorking(workHood);
            }

            while (value < session.MockTasks.Count())
            {
                session.MockTasks.Remove(session.MockTasks.First().Key);
            }

            Present.SendCommand(new UICommand_Refresh());
        }
    }

    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            for (int i = 0; i < 3; i++)
            {
                var task = session.GenerateTask();

                var workHood = session.GenerateWorkHood();
                task.Working = session.GenerateProgressWorking(workHood);
            }

            return new SessionModel() { Session = session };
        }
    }
}