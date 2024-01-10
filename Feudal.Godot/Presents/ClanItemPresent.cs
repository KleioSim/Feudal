namespace Feudal.Godot.Presents;

partial class ClanItemPresent : PresentControl<ClanItemView, ISessionModel>
{
    protected override void Initialize(ClanItemView view, ISessionModel model)
    {

    }

    protected override void Update(ClanItemView view, ISessionModel model)
    {
        var clan = model.Session.Clans[view.Id];

        view.Label.Text = clan.Name;
        view.PopCount.Text = clan.PopCount.ToString();
    }
}
