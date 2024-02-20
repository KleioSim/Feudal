using Feudal.Interfaces;

namespace Feudal.Clans;

internal partial class ClanManager
{
    public void Initialize()
    {
        for (int i = 0; i < 3; i++)
        {
            var clan = new Clan($"clan{i}", i * 1000);
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
}