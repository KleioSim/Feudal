using System.Linq;

namespace Feudal.Godot.Presents;

partial class ClanArrayPresent : PresentControl<ClanArrayView, ISessionModel>
{
    protected override void Initialize(ClanArrayView view, ISessionModel model)
    {

    }

    protected override void Update(ClanArrayView view, ISessionModel model)
    {
        view.Container.Refresh(model.Session.Clans.Keys.ToHashSet());
    }
}