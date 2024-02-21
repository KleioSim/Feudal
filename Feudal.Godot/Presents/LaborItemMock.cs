using System.Linq;

namespace Feudal.Godot.Presents;

public partial class LaborItemMock : MockControl<LaborItemView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            var clan = session.GenerateClan();
            View.Id = clan.Id;

            var labors = clan.GenerateLabors(3);
            labors.First().Task = session.GenerateTask();

            return new SessionModel() { Session = session };
        }
    }
}