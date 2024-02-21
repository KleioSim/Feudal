using Feudal.Interfaces;

namespace Feudal.Clans;

internal partial class ClanManager
{
    public static IFinder Finder
    {
        get => Clan.Finder;
        set => Clan.Finder = value;
    }

    private Dictionary<string, Labor> laborDict = new Dictionary<string, Labor>();

    public ClanManager()
    {
        LaborManager.OnAddLabor = (labor) =>
        {
            laborDict.Add(labor.Id, labor);
        };

        LaborManager.OnRemoveLabor = (labor) =>
        {
            laborDict.Remove(labor.Id);
        };
    }

    public void Initialize()
    {
        for (int i = 0; i < 3; i++)
        {
            var clan = new Clan($"clan{i}", (i + 1) * 100);
            dict.Add(clan.Id, clan);
        }
    }

    public void OnNextTurn(IDate date)
    {
        ProductProcess(date);
    }

    private void ProductProcess(IDate date)
    {
        if (date.Day == 1)
        {
            foreach (var product in dict.Values.SelectMany(x => x.Products.Values).OfType<Product>())
            {
                product.Current += product.Surplus;
            }
        }
    }

    public ILabor FindLabor(string id)
    {
        return laborDict[id];
    }
}