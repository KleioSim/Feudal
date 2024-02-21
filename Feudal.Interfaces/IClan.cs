namespace Feudal.Interfaces;

using System.Collections.Generic;

public interface IClan
{
    string Id { get; }
    string Name { get; }
    int PopCount { get; }

    IEnumerable<ILabor> Labors { get; }

    IReadOnlyDictionary<ProductType, IProduct> Products { get; }
}
