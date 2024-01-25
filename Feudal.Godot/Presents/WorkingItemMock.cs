using Feudal.Interfaces;
using Godot;
using System.Collections.Generic;

namespace Feudal.Godot.Presents;

partial class WorkingItemMock : MockControl<WorkingItemView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            var workHood = session.GenerateWorkHood();
            View.WorkHoodId = workHood.Id;

            var working = session.GenerateWorking(workHood);
            View.Id = working.Id;

            var task = session.GenerateTask();
            task.ClanId = session.GenerateClan().Id;
            task.WorkHoodId = workHood.Id;

            return new SessionModel() { Session = session };
        }
    }
}

class MockWorking : IProgressWorking
{
    public string Id { get; set; }

    public string Name => Id;

    private static int Count = 0;

    public MockWorking()
    {
        Id = $"WG{Count++}";
    }

    public void Finished(IWorkHood workHood)
    {
        GD.Print($"Working:{Id} Finsihed, WorkHood:{workHood}");
    }

    public float GetStep(IWorkHood workHood)
    {
        return 3;
    }

    public IEffectValue GetEffectValue(string workHoodId)
    {
        return new MockEffectValue();
    }
}

class MockEffectValue : IEffectValue
{
    public float BaseValue { get; } = 100;

    public IEnumerable<IEffect> Effects { get; } = new IEffect[]
    {
        new MockEffect() { Desc = "Effect1", Percent = 20},
        new MockEffect() { Desc = "Effect1", Percent = -10}
    };
}

class MockEffect : IEffect
{
    public string Desc { get; set; }

    public float Percent { get; set; }
}