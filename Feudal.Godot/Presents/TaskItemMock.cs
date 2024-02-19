using Feudal.Godot.UICommands;
using Godot;

namespace Feudal.Godot.Presents;

[Tool]
partial class TaskItemMock : MockControl<TaskItemView, ISessionModel>
{
    private TaskMock Task => Present.Model.Session.Tasks[View.Id] as TaskMock;

    [Export]
    public float Precent
    {
        get
        {
            return ((MockProgressWorking)Task.Working).Percent;
        }
        set
        {
            ((MockProgressWorking)Task.Working).Percent = value;

            Present.SendCommand(new UICommand_Refresh());
        }
    }

    [Export]
    public string Desc
    {
        get
        {
            return Task.Desc;
        }
        set
        {
            Task.Desc = value;

            Present.SendCommand(new UICommand_Refresh());
        }
    }

    public override ISessionModel Mock
    {
        get
        {

            var session = new SessionMock();

            var task = session.GenerateTask();
            View.Id = task.Id;

            var workHood = session.GenerateWorkHood();
            task.Working = session.GenerateProgressWorking(workHood);

            return new SessionModel() { Session = session };
        }
    }
}
