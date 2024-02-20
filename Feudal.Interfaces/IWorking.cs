using Feudal.Commands;

namespace Feudal.Interfaces;

public interface IWorking
{
    string Id { get; }

    string Name { get; }

    IWorkHood WorkHood { get; }
}

public interface IProductWorking : IWorking
{
    ProductType ProductType { get; }
    IEffectValue GetEffectValue(string workHoodId);
}

public interface IProgressWorking : IWorking
{
    float Percent { get; set; }

    IEffectValue GetEffectValue();

    void OnFinish(Action<ICommand> SendCommand);
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