using Feudal.Interfaces;

namespace Feudal.WorkHoods.Workings;

internal class Hunting : Working, IProductWorking
{
    public ProductType ProductType => ProductType.Food;

    public Hunting(IWorkHood workHood) : base(workHood)
    {
    }

    public IEffectValue GetEffectValue()
    {
        return new EffectValue() { BaseValue = 2, Effects = new IEffect[] { } };
    }
}
