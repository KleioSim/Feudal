using System.Collections.Generic;

namespace Feudal.Godot.Presents;

partial class ClanItemMock : MockControl<ClanItemView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var clan = new ClanMock();
            View.Id = clan.Id;

            var session = new SessionMock();
            session.MockClans.Add(clan.Id, clan);

            return new ModelMock() { Session = session };
        }
    }
}
