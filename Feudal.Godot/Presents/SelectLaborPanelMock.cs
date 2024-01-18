namespace Feudal.Godot.Presents;

public partial class SelectLaborPanelMock : MockControl<SelectLaborPanelView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            for (int i = 0; i < 3; i++)
            {
                var clan = new ClanMock();
                session.MockClans.Add(clan.Id, clan);
            }

            return new SessionModel() { Session = session };
        }
    }
}