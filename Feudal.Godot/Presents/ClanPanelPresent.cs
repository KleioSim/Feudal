namespace Feudal.Godot.Presents;

partial class ClanPanelPresent : PresentControl<ClanPanelView, ISessionModel>
{
    protected override void Initialize(ClanPanelView view, ISessionModel model)
    {

    }

    protected override void Update(ClanPanelView view, ISessionModel model)
    {
        var clan = model.Session.Clans[view.Id];

        view.Title.Text = clan.Name;
        view.PopCount.Text = clan.PopCount.ToString();
        view.LaborCount.Text = clan.Labor.TotalCount.ToString();
    }
}
