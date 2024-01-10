using System;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class WorkingItemPresent : PresentControl<WorkingItemView, ISessionModel>
{
    protected override void Initialize(WorkingItemView view, ISessionModel model)
    {

    }

    protected override void Update(WorkingItemView view, ISessionModel model)
    {
        var working = model.Session.Workings[view.Id];
        view.Button.Text = working.Name;
    }
}
