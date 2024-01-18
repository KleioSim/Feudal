using Feudal.Interfaces;
using Feudal.Sessions;
using Feudal.TerrainBuilders;
using System.Threading.Tasks;

namespace Feudal.SessionBuilders;

public class SessionBuilder
{
    public static ISession BuildDebug()
    {
        var session = new Session()
        {
            Date = new Date(),
        };

        for (int i = 0; i < 3; i++)
        {
            var clan = new Clan($"clan{i}", i * 1000);
            session.clans.Add(clan.Id, clan);
        }

        session.PlayerClan = session.clans.Values.First();

        var terrainBuilder = new TerrainBuilder(TerrainType.Hill);
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                var pos = (x, y);
                var terrain = new Terrain(pos, terrainBuilder.Build(pos));
                if (pos == (0, 0))
                {
                    terrain.IsDiscoverd = true;
                }

                session.terrains.Add(terrain.Position, terrain);
            }
        }

        return session;
    }
}

internal class Terrain : ITerrain
{
    public (int x, int y) Position { get; }

    public IReadOnlySet<IResource> Resources => resources;

    public TerrainType TerrainType { get; private set; }

    public bool IsDiscoverd { get; set; }

    public string WorkHoodId { get; private set; }

    private HashSet<IResource> resources = new HashSet<IResource>();

    public Terrain((int x, int y) position, TerrainType terrainType)
    {
        this.Position = position;
        this.TerrainType = terrainType;
    }
}

internal class Clan : IClan
{
    public static int count;

    public string Id { get; }

    public string Name { get; private set; }

    public int PopCount { get; private set; }

    public ILabor Labor { get; }

    public IReadOnlyDictionary<ProductType, IProduct> Products { get; } = Enum.GetValues<ProductType>().ToDictionary(k => k, v => new Product(v) as IProduct);

    public Clan(string name, int popCount)
    {
        this.Name = name;
        this.Id = $"CLAN_{count++}";

        this.PopCount = popCount;
        this.Labor = new Labor(this);
    }
}

internal class Product : IProduct
{
    public ProductType Type { get; }

    public float Current { get; private set; }

    public float Surplus { get; private set; }

    public Product(ProductType type)
    {
        this.Type = type;
    }
}

internal class Labor : ILabor
{
    public int TotalCount => from.PopCount / 100;

    private IClan from;

    public Labor(IClan from)
    {
        this.from = from;
    }
}

class Date : IDate
{
    public int Year { get; private set; }

    public int Month { get; private set; }

    public int Day { get; private set; }

    public Date()
    {
        Year = 1;
        Month = 1;
        Day = 1;
    }

    void OnDayInc()
    {
        Day += 10;

        if (Day > 30)
        {
            Month += 1;
            Day = 1;
        }

        if (Month > 12)
        {
            Year += 1;
            Month = 1;
        }
    }
}