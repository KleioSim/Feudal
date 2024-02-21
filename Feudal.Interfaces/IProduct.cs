namespace Feudal.Interfaces;

public interface IProduct
{
    ProductType Type { get; }

    float Current { get; }

    float Surplus { get; }

    IReadOnlyDictionary<object, Func<float>> Incomes { get; }
    IReadOnlyDictionary<object, Func<float>> Outputs { get; }
}
