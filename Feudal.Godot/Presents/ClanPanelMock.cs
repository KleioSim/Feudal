namespace Feudal.Godot.Presents;

partial class ClanPanelMock : MockControl<ClanPanelView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var clan = new ClanMock();
            clan.LaborMock.TotalCount = 10;

            View.Id = clan.Id;

            var session = new SessionMock();
            session.MockClans.Add(clan.Id, clan);

            return new SessionModel() { Session = session };
        }
    }
}