using Feudal.Interfaces;

namespace Feudal.WorkHoods.Workings;

internal class BuildingFarm : Working, IProgressWorking
{
    public BuildingFarm(IWorkHood workHood) : base(workHood)
    {
    }

    public float Percent { get; set; }

    public void Finished()
    {

    }

    public IEffectValue GetEffectValue()
    {
        return new EffectValue() { BaseValue = 10, Effects = new IEffect[] { } };
    }
}

class EffectValue : IEffectValue
{
    public float BaseValue { get; init; }

    public IEnumerable<IEffect> Effects { get; init; }
}

class Effect : IEffect
{
    public string Desc { get; init; }

    public float Percent { get; init; }
}