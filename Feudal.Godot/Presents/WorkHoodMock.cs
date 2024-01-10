using System.Linq;

namespace Feudal.Godot.Presents;

partial class WorkHoodMock : MockControl<WorkHoodView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            var workHood = new MockWorkHood();
            View.Id = workHood.Id;

            session.MockWorkHoods.Add(workHood.Id, workHood);

            for (int i = 0; i < 2; i++)
            {
                var working = new MockWorking();

                workHood.MockOptionWorkings.Add(working);
                session.MockWorkings.Add(working.Id, working);
            }

            workHood.CurrentWorking = workHood.OptionWorkings.First();

            return new ModelMock() { Session = session };
        }
    }
}