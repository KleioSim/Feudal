using System.Linq;

namespace Feudal.Godot.Presents;

public partial class SelectLaborPanelPresent : PresentControl<SelectLaborPanelView, ISessionModel>
{

    protected override void Initialize(SelectLaborPanelView view, ISessionModel model)
    {

    }

    protected override void Update(SelectLaborPanelView view, ISessionModel model)
    {
        view.Container.Refresh(model.Session.Clans.Keys.Select(x => x as object).ToHashSet());
    }
}
