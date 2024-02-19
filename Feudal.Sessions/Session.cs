using Feudal.Interfaces;
using Feudal.Resources;
using Feudal.Tasks;
using Feudal.Terrains;
using Feudal.WorkHoods;
using Feudal.Workings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Feudal.Sessions;

partial class Session : ISession
{
    public IClan PlayerClan { get; set; }

    public IDate Date { get; init; }

    public IReadOnlyDictionary<object, ITask> Tasks => taskManager;

    public IReadOnlyDictionary<object, IResource> Resources => resourceManager;

    public IReadOnlyDictionary<(int x, int y), ITerrain> Terrains => terrainManager;

    public IReadOnlyDictionary<object, IWorkHood> WorkHoods => workHoodManager;

    //public IReadOnlyDictionary<object, IWorking> Workings => workingManager;

    public IReadOnlyDictionary<object, IClan> Clans => clans;


    internal readonly Dictionary<object, IClan> clans = new Dictionary<object, IClan>();

    internal readonly TaskManager taskManager = new TaskManager();
    internal readonly TerrainManager terrainManager = new TerrainManager();
    internal readonly WorkHoodManager workHoodManager = new WorkHoodManager();
    internal readonly ResourceManager resourceManager = new ResourceManager();

    //internal readonly WorkingManager workingManager;

    public Session()
    {
        //workingManager = new WorkingManager(this);

        var finder = new Finder(this);

        WorkHoodManager.Finder = finder;
        TerrainManager.Finder = finder;
        TaskManager.Finder = finder;
        ResourceManager.Finder = finder;
    }

    public void OnCommand(Command command, string[] parameters)
    {
        switch (command)
        {
            case Command.NextTurn:
                {
                    taskManager.OnNextTurn();
                    Date.OnDaysInc();
                }
                break;
            case Command.OccupyLabor:
                {
                    taskManager.CreateTask(parameters[0], parameters[1]);
                }
                break;
            case Command.ReleaseLabor:
                {
                    taskManager.RelaseTask(parameters[0], parameters[1]);
                }
                break;
        }
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

    public void OnDaysInc()
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
