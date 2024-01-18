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
            return Task.Percent;
        }
        set
        {
            Task.Percent = value;

            Present.SendCommand(new UIRefreshCommand());
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

            Present.SendCommand(new UIRefreshCommand());
        }
    }

    public override ISessionModel Mock
    {
        get
        {
            var task = new TaskMock();
            View.Id = task.Id;

            var session = new SessionMock();
            session.MockTasks.Add(task.Id, task);

            return new SessionModel() { Session = session };
        }
    }
}
