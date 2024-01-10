namespace Feudal.Godot.Presents;

partial class ClanArrayMock : MockControl<ClanArrayView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            for(int i=0; i<3; i++)
            {
                var clan = new ClanMock();
                session.MockClans.Add(clan.Id, clan);
            }

            return new ModelMock() { Session = session };
        }
    }
}