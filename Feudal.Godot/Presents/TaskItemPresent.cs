using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class TaskItemPresent : PresentControl<TaskItemView, ISessionModel>
{

    protected override void Initialize(TaskItemView view, ISessionModel model)
    {

    }

    protected override void Update(TaskItemView view, ISessionModel model)
    {
        var taskObj = model.Session.Tasks[view.Id];

        view.Label.Text = taskObj.Desc;
        view.Progress.Value = taskObj.Percent;
    }
}

public interface ISessionModel : IModel
{
    ISession Session { get; }
}