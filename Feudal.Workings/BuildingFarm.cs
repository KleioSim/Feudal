using Feudal.Interfaces;

namespace Feudal.Workings;

internal class BuildingFarm : Working, IProgressWorking
{
    public BuildingFarm(ISession session) : base(session)
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