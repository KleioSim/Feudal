namespace Feudal.Godot.Presents;

partial class ClanPanelMock : MockControl<ClanPanelView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            var clan = session.GenerateClan();
            View.Id = clan.Id;

            clan.GenerateLabors(3);

            return new SessionModel() { Session = session };
        }
    }
}