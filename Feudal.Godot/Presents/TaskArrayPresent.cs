using System.Linq;

namespace Feudal.Godot.Presents;

partial class TaskArrayPresent : PresentControl<TaskArrayView, ISessionModel>
{
    protected override void Initialize(TaskArrayView view, ISessionModel model)
    {

    }

    protected override void Update(TaskArrayView view, ISessionModel model)
    {
        view.Container.Refresh(model.Session.Tasks.Values.Select(x => x.Id as object).ToHashSet());
    }
}
