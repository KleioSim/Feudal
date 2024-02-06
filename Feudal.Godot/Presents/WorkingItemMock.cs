using Feudal.Godot.UICommands;
using Feudal.Interfaces;
using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class WorkingItemMock : MockControl<WorkingItemView, ISessionModel>
{

    private string workingType;
    public string WorkingType
    {
        get => workingType;
        set
        {
            if (workingType == value)
            {
                return;
            }

            workingType = value;

            var working = Present.Model.Session.Workings.Values.SingleOrDefault(x => x.GetType().Name == workingType);
            View.Id = working.Id;

            Present.SendCommand(new UICommand_Refresh());
        }
    }

    public override Array<Dictionary> _GetPropertyList()
    {
        var properties = new Array<Dictionary>();

        var workingTypes = Present.Model.Session.Workings.Select(x => x.Value.GetType().Name);

        properties.Add(new Dictionary()
        {
            { "name", nameof(WorkingType) },
            { "type", (int)Variant.Type.String },
            { "usage", (int)PropertyUsageFlags.Default }, // See above assignment.
            { "hint", (int)PropertyHint.Enum },
            { "hint_string", string.Join(",", workingTypes) }
        });

        return properties;
    }

    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            var workHood = session.GenerateWorkHood();

            session.GenerateProductWorking(workHood);
            session.GenerateProgressWorking(workHood);

            View.Id = workHood.OptionWorkings.Last();
            workingType = workHood.OptionWorkings.Last().Name;

            var task = session.GenerateTask();
            task.Clan = session.GenerateClan();
            task.WorkHood = workHood;

            return new SessionModel() { Session = session };
        }
    }
}

class MockProgressWorking : IProgressWorking
{
    public string Id { get; set; }

    public string Name => Id;

    public IWorkHood WorkHood { get; set; }

    private static int Count = 0;

    public MockProgressWorking()
    {
        Id = $"PROGRESS_WORKING_{Count++}";
    }

    public void Finished()
    {
        GD.Print($"Working:{Id} Finsihed, WorkHood:{WorkHood.Id}");
    }

    public IEffectValue GetEffectValue()
    {
        return new MockEffectValue();
    }
}

class MockProductWorking : IProductWorking
{
    public string Id { get; set; }

    public IWorkHood WorkHood { get; set; }

    public string Name => Id;

    public ProductType ProductType => ProductType.Food;

    private static int Count = 0;

    public MockProductWorking()
    {
        Id = $"PRODUCT_WORKING_{Count++}";
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