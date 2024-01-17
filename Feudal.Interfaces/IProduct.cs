namespace Feudal.Interfaces;

public interface IProduct
{
    ProductType Type { get; }
    float Current { get; }
    float Surplus { get; }
}
