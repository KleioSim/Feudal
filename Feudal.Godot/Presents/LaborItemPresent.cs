using System.Linq;

namespace Feudal.Godot.Presents;

public partial class LaborItemPresent : PresentControl<LaborItemView, ISessionModel>
{
    protected override void Initialize(LaborItemView view, ISessionModel model)
    {

    }

    protected override void Update(LaborItemView view, ISessionModel model)
    {
        var clan = model.Session.Clans[view.Id];

        view.ClanName.Text = clan.Name;

        var idleLabors = clan.Labors.Where(x => x.Task == null);

        view.Button.Disabled = !idleLabors.Any();

        view.IdleCount.Text = idleLabors.Count().ToString();
        view.TotalCount.Text = clan.Labors.Count().ToString();

    }
}
