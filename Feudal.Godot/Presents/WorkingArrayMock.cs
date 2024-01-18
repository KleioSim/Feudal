using Feudal.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class WorkingArrayMock : MockControl<WorkingArrayView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            var workHood = new MockWorkHood();
            View.WorkHoodId = workHood.Id;

            session.MockWorkHoods.Add(workHood.Id, workHood);

            for (int i = 0; i < 3; i++)
            {
                var working = new MockWorking();

                workHood.MockOptionWorkings.Add(working);
                session.MockWorkings.Add(working.Id, working);
            }

            workHood.CurrentWorking = workHood.OptionWorkings.First();

            return new SessionModel() { Session = session };
        }
    }
}

class MockWorkHood : IWorkHood
{
    private static int count = 0;

    public string Id { get; } = $"WH{count}";

    public IWorking CurrentWorking { get; set; }

    public IEnumerable<IWorking> OptionWorkings => MockOptionWorkings;

    public List<IWorking> MockOptionWorkings = new List<IWorking>();
}