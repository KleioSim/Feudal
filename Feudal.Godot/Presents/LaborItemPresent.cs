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
        view.Count.Text = clan.Labor.TotalCount.ToString();
    }
}
