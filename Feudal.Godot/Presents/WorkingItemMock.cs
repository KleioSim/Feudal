using Feudal.Commands;
using Feudal.Godot.UICommands;
using Feudal.Interfaces;
using Godot;
using Godot.Collections;
using System;
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

            var working = workHood.OptionWorkings.SingleOrDefault(x => x.GetType().Name == workingType);
            View.Id = working;

            Present.SendCommand(new UICommand_Refresh());
        }
    }

    public override Array<Dictionary> _GetPropertyList()
    {
        var properties = new Array<Dictionary>();

        var workingTypes = workHood.OptionWorkings.Select(x => x.GetType().Name);

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

            return new SessionModel() { Session = session };
        }
    }

    private IWorkHood workHood => Present.Model.Session.WorkHoods.Values.SingleOrDefault();
}

class MockProgressWorking : IProgressWorking
{
    public string Id { get; set; }

    public string Name => Id;

    public IWorkHood WorkHood { get; set; }

    public float Percent { get; set; } = 33f;

    private static int Count = 0;

    public MockProgressWorking()
    {
        Id = $"PROGRESS_WORKING_{Count++}";
    }

    public IEffectValue GetEffectValue()
    {
        return new MockEffectValue();
    }

    public void OnFinish(Action<ICommand> SendCommand)
    {
        GD.Print($"Working:{Id} Finsihed, WorkHood:{WorkHood.Id}");
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