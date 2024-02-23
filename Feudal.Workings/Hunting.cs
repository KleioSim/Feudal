using Feudal.Interfaces;

namespace Feudal.Workings;

public class Hunting : Working, IProductWorking
{
    public ProductType ProductType => ProductType.Food;

    public Hunting(IWorkHood workHood) : base(workHood)
    {
    }

    public override IEffectValue GetEffectValue()
    {
        return new EffectValue() { BaseValue = 2, Effects = new IEffect[] { } };
    }
}
