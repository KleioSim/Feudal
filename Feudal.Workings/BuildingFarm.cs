using Feudal.Interfaces;

namespace Feudal.Workings;

internal class BuildingFarm : Working, IProgressWorking
{
    public BuildingFarm(ISession session) : base(session)
    {
    }

    public void Finished(IWorkHood workHood)
    {

    }

    public IEffectValue GetEffectValue(string workHoodId)
    {
        return new EffectValue() { BaseValue = 10, Effects = new IEffect[] { } };
    }

    public float GetStep(IWorkHood workHood)
    {
        return GetEffectValue(workHood.Id).Value;
    }
}