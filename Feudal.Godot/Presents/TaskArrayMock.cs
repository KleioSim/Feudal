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
            var dict = Present.Model.Session.Tasks as Dictionary<object, ITask>;
            if (value > dict.Count())
            {
                foreach (var newTask in Enumerable.Range(0, value - dict.Count()).Select(_ => new TaskMock()).ToArray())
                {
                    dict.Add(newTask.Id, newTask);
                }
            }
            while (value < dict.Count())
            {
                dict.Remove(dict.First().Key);
            }

            Present.SendCommand(new UIRefreshCommand());
        }
    }

    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            var tasks = new List<TaskMock>()
            {
                new TaskMock(),
                new TaskMock(),
                new TaskMock(),
            };

            foreach (var task in tasks)
            {
                session.MockTasks.Add(task.Id, task);
            }

            return new ModelMock() { Session = session };
        }
    }
}