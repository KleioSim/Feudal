namespace Feudal.Interfaces;

using System.Collections.Generic;

public interface IClan
{
    string Id { get; }
    string Name { get; }
    int PopCount { get; }

    ILabor Labor { get; }

    IReadOnlyDictionary<ProductType, IProduct> Products { get; }
}
