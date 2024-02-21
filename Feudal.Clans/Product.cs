using Feudal.Interfaces;

namespace Feudal.Clans;

internal class Product : IProduct
{
    public ProductType Type { get; }

    public float Current { get; set; }

    public float Surplus => Incomes.Sum(x => x.Value.Invoke()) - Outputs.Sum(x => x.Value.Invoke());

    public IReadOnlyDictionary<object, Func<float>> Incomes => incomes;

    public IReadOnlyDictionary<object, Func<float>> Outputs => outputs;

    private Dictionary<object, Func<float>> incomes = new Dictionary<object, Func<float>>();
    private Dictionary<object, Func<float>> outputs = new Dictionary<object, Func<float>>();

    public Product(ProductType type)
    {
        this.Type = type;
    }

    internal void AddOutput(object key, Func<float> value)
    {
        outputs.Add(key, value);
    }

    internal void RemoveOutput(object key)
    {
        outputs.Remove(key);
    }


    internal void AddIncome(object key, Func<float> value)
    {
        incomes.Add(key, value);
    }

    internal void RemoveIncome(object key)
    {
        incomes.Remove(key);
    }
}
