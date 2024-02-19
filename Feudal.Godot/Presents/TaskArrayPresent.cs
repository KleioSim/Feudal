using Feudal.Interfaces;
using Godot;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class TaskArrayPresent : PresentControl<TaskArrayView, ISessionModel>
{
    protected override void Initialize(TaskArrayView view, ISessionModel model)
    {
        view.Container.OnAddedItem = (item) =>
        {
            item.Button.Pressed += () =>
            {
                var task = model.Session.Tasks[item.Id];
                switch (task.Working.WorkHood)
                {
                    case ITerrainWorkHood terrainWorkHood:
                        var terrainPos = terrainWorkHood.Position;
                        view.EmitSignal(TaskArrayView.SignalName.ShowTerrain, new Vector2I(terrainPos.x, terrainPos.y));
                        break;
                }
            };
        };
    }

    protected override void Update(TaskArrayView view, ISessionModel model)
    {
        view.Container.Refresh(model.Session.Tasks.Values.Select(x => x.Id as object).ToHashSet());
    }
}
