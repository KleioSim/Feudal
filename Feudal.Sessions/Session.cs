using Feudal.Interfaces;
using Feudal.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Feudal.Sessions;

class Session : ISession
{
    public IClan PlayerClan { get; set; }

    public IDate Date { get; init; }

    public IReadOnlyDictionary<object, ITask> Tasks => taskManager;

    public IReadOnlyDictionary<object, IResource> Resources => resource;

    public IReadOnlyDictionary<(int x, int y), ITerrain> Terrains => terrains;

    public IReadOnlyDictionary<object, IWorkHood> WorkHoods => workHoods;

    public IReadOnlyDictionary<object, IWorking> Workings => workings;

    public IReadOnlyDictionary<object, IClan> Clans => clans;

    internal readonly Dictionary<object, ITask> tasks = new Dictionary<object, ITask>();
    internal readonly Dictionary<object, IResource> resource = new Dictionary<object, IResource>();
    internal readonly Dictionary<(int x, int y), ITerrain> terrains = new Dictionary<(int x, int y), ITerrain>();
    internal readonly Dictionary<object, IWorkHood> workHoods = new Dictionary<object, IWorkHood>();
    internal readonly Dictionary<object, IWorking> workings = new Dictionary<object, IWorking>();
    internal readonly Dictionary<object, IClan> clans = new Dictionary<object, IClan>();

    private TaskManager taskManager = new TaskManager();

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

internal class Working : IWorking
{
    public string Id { get; }

    public string Name => Id;

    protected ISession session;

    public Working(ISession session)
    {
        Id = this.GetType().Name;

        this.session = session;

    }
}

internal class DiscoverWorking : Working, IProgressWorking
{
    public DiscoverWorking(ISession session) : base(session)
    {
    }

    public void Finished(IWorkHood workHood)
    {
        if (workHood is not TerrainWorkHood terrainWorkHood)
        {
            throw new Exception();
        }

        ((Terrain)session.Terrains[terrainWorkHood.Position]).IsDiscoverd = true;
    }

    public float GetStep(IWorkHood workHood)
    {
        if (workHood is not TerrainWorkHood terrainWorkHood)
        {
            throw new Exception();
        }

        var baseValue = 20f;
        var terrain = session.Terrains[terrainWorkHood.Position];

        var effect = GetEffect(terrain.TerrainType);
        return baseValue * (1 + effect);
    }

    public float GetEffect(TerrainType terrainType)
    {
        switch (terrainType)
        {
            case TerrainType.Plain:
                return 0;
            case TerrainType.Hill:
                return -0.6f;
            case TerrainType.Mountion:
                return -0.9f;
            case TerrainType.Lake:
                return -0.3f;
            case TerrainType.Marsh:
                return -0.5f;
            default:
                throw new Exception();
        }
    }
}

internal class WorkHood : IWorkHood
{
    public static int Count;

    public string Id { get; }

    public IWorking CurrentWorking { get; private set; }

    public IEnumerable<IWorking> OptionWorkings => optionWorkings.Where(x => x != CurrentWorking);

    private IEnumerable<IWorking> optionWorkings;

    internal void UpdateWorkings(IEnumerable<IWorking> workings)
    {
        optionWorkings = workings;

        if (optionWorkings.Contains(CurrentWorking))
        {
            return;
        }

        CurrentWorking = optionWorkings.First();
    }

    public WorkHood()
    {
        Id = $"WorkHood_{Count++}";
    }
}

class TerrainWorkHood : WorkHood, ITerrainWorkHood
{
    public (int x, int y) Position { get; }

    public TerrainWorkHood((int x, int y) position)
    {
        this.Position = position;
    }
}

internal class Terrain : ITerrain
{
    //public delegate void DelegateOccupyOrUpdateWorkHood(ref string Id, IEnumerable<IWorking> workings);

    //public static DelegateOccupyOrUpdateWorkHood OccupyOrUpdateWorkHood;
    //public static Action<string> ReleaseWorkHood;

    //public static IWorking DiscoverWorking { get; set; }

    public static Func<ITerrain, string> GetWorkHoodId;

    public (int x, int y) Position { get; }

    public IReadOnlySet<IResource> Resources => resources;

    public TerrainType TerrainType { get; private set; }

    public bool IsDiscoverd { get; set; }

    public string WorkHoodId => GetWorkHoodId(this);

    //{
    //    get
    //    {
    //        var workingOptions = GetWorkingOptions();
    //        if (workingOptions.Any())
    //        {
    //            OccupyOrUpdateWorkHood(ref workHoodId, workingOptions);
    //        }
    //        else
    //        {
    //            ReleaseWorkHood(workHoodId);
    //            workHoodId = null;
    //        }

    //        return workHoodId;
    //    }
    //}

    //private string workHoodId;

    private HashSet<IResource> resources = new HashSet<IResource>();

    public Terrain((int x, int y) position, TerrainType terrainType)
    {
        this.Position = position;
        this.TerrainType = terrainType;
    }

    //private IEnumerable<IWorking> GetWorkingOptions()
    //{
    //    if (!IsDiscoverd)
    //    {
    //        return new[] { DiscoverWorking };
    //    }

    //    return Resources.Select(x => x.GetWorkings()).SelectMany(x => x);
    //}
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
