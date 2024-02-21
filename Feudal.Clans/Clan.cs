using Feudal.Interfaces;
using System.Linq;

namespace Feudal.Clans;

internal class Clan : IClan
{
    public static IFinder Finder { get; set; }

    public static int count;

    public string Id { get; }

    public string Name { get; private set; }

    public int PopCount { get; private set; }

    public IReadOnlyDictionary<ProductType, IProduct> Products { get; } = Enum.GetValues<ProductType>().ToDictionary(k => k, v => new Product(v) as IProduct);

    public IEnumerable<ILabor> Labors => laborManager;

    private LaborManager laborManager;

    public Clan(string name, int popCount)
    {
        this.Name = name;
        this.Id = $"CLAN_{count++}";

        this.PopCount = popCount;

        laborManager = new LaborManager(this);

        var foodProduct = Products[ProductType.Food] as Product;
        foodProduct!.AddOutput(this, () => Labors.Count());
    }

    public void AddProductTask(ITask task)
    {
        if (task.Working is not IProductWorking productWorking)
        {
            throw new Exception();
        }

        var product = Products[productWorking.ProductType] as Product;
        product!.AddIncome(task, () => task.Working.GetEffectValue().Value);
    }

    public void RemoveProductTask(ITask task)
    {
        if (task.Working is not IProductWorking productWorking)
        {
            throw new Exception();
        }

        var product = Products[productWorking.ProductType] as Product;
        product!.RemoveIncome(task);
    }
}
