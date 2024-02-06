namespace Feudal.Interfaces;

public interface IWorking
{
    string Id { get; }

    string Name { get; }

    IWorkHood WorkHood { get; set; }
}

public interface IProductWorking : IWorking
{
    ProductType ProductType { get; }
    IEffectValue GetEffectValue(string workHoodId);
}

public interface IProgressWorking : IWorking
{
    void Finished(IWorkHood workHood);
    float GetStep(IWorkHood workHood);

    IEffectValue GetEffectValue(string workHoodId);
}

public interface IEffectValue
{
    float Value => BaseValue * (1 + Effects.Sum(x => x.Percent) / 100);

    float BaseValue { get; }

    IEnumerable<IEffect> Effects { get; }
}

public interface IEffect
{
    string Desc { get; }
    float Percent { get; }
}