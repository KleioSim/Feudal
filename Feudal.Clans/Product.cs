using Feudal.Interfaces;

namespace Feudal.Clans;

internal class Product : IProduct
{
    public ProductType Type { get; }

    public float Current { get; set; }

    public float Surplus
    {
        get
        {
            return productTasks.Select(x => x.Working).OfType<IProductWorking>().Sum(x => x.GetEffectValue().Value);
        }
    }

    private List<ITask> productTasks = new List<ITask>();

    public Product(ProductType type)
    {
        this.Type = type;
    }

    internal void AddProductTask(ITask task)
    {
        productTasks.Add(task);
    }

    internal void RemoveProductTask(ITask task)
    {
        productTasks.Remove(task);
    }
}
