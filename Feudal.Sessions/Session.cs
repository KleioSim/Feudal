using Feudal.Interfaces;
using Feudal.Tasks;
using Feudal.Terrains;
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

    public IReadOnlyDictionary<(int x, int y), ITerrain> Terrains => terrainManager;

    public IReadOnlyDictionary<object, IWorkHood> WorkHoods => workHoods;

    public IReadOnlyDictionary<object, IWorking> Workings => workings;

    public IReadOnlyDictionary<object, IClan> Clans => clans;

    internal readonly Dictionary<object, IResource> resource = new Dictionary<object, IResource>();
    internal readonly Dictionary<(int x, int y), ITerrain> terrains = new Dictionary<(int x, int y), ITerrain>();
    internal readonly Dictionary<object, IWorkHood> workHoods = new Dictionary<object, IWorkHood>();
    internal readonly Dictionary<object, IWorking> workings = new Dictionary<object, IWorking>();
    internal readonly Dictionary<object, IClan> clans = new Dictionary<object, IClan>();

    internal readonly TaskManager taskManager = new TaskManager();
    internal readonly TerrainManager terrainManager = new TerrainManager();


    public Session()
    {
        var finder = new Finder()
        {
            FindWorking = (name) => workings[name],
            FindTerrain = (position) => Terrains[position],
            FindWorkHoodId = (position) =>
            {
                var workHood = workHoods.Values.OfType<ITerrainWorkHood>().SingleOrDefault(x => x.Position == position);
                if (workHood == null)
                {
                    workHood = new TerrainWorkHood(position);
                    workHoods.Add(workHood.Id, workHood);
                }

                return workHood.Id;
            }
        };

        TerrainWorkHood.Finder = finder;
        TerrainManager.Finder = finder;
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

    class Finder : IFinder
    {
        public Func<(int x, int y), ITerrain> FindTerrain { get; internal set; }

        public Func<(int x, int y), string> FindWorkHoodId { get; internal set; }

        public Func<string, IWorking> FindWorking { get; internal set; }
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

class EffectValue : IEffectValue
{
    public float BaseValue { get; init; }

    public IEnumerable<IEffect> Effects { get; init; }
}

class Effect : IEffect
{
    public string Desc { get; init; }

    public float Percent { get; init; }
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

        ((TerrainManager)session.Terrains).SetDiscoverd(terrainWorkHood.Position);

        //((Terrain)session.Terrains[terrainWorkHood.Position]).IsDiscoverd = true;
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
                return -50f;
            case TerrainType.Mountion:
                return -90f;
            case TerrainType.Lake:
                return -30f;
            case TerrainType.Marsh:
                return -60f;
            default:
                throw new Exception();
        }
    }

    public IEffectValue GetEffectValue(string workHoodId)
    {
        var workHood = session.WorkHoods[workHoodId];

        if (workHood is not TerrainWorkHood terrainWorkHood)
        {
            throw new Exception();
        }

        var terrain = session.Terrains[terrainWorkHood.Position];

        var effects = new[] { new Effect() { Desc = terrain.TerrainType.ToString(), Percent = GetEffect(terrain.TerrainType) } };

        var effectValue = new EffectValue() { BaseValue = 20, Effects = effects };

        return effectValue;
    }
}

internal class WorkHood : IWorkHood
{
    public static int Count;

    public string Id { get; }

    public IWorking CurrentWorking { get; private set; }

    public virtual IEnumerable<IWorking> OptionWorkings
    {
        get => optionWorkings.Where(x => x != CurrentWorking);
        set
        {
            optionWorkings = value;

            if (optionWorkings.Contains(CurrentWorking))
            {
                return;
            }

            CurrentWorking = optionWorkings.FirstOrDefault();
        }
    }

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
    public static IFinder Finder { get; set; }

    public (int x, int y) Position { get; }

    public TerrainWorkHood((int x, int y) position)
    {
        this.Position = position;
    }

    public override IEnumerable<IWorking> OptionWorkings
    {
        get
        {
            var terrain = Finder.FindTerrain(Position);

            base.OptionWorkings = !terrain.IsDiscoverd ? new[] { Finder.FindWorking(typeof(DiscoverWorking).Name) } : terrain.Resources.Select(x => x.GetWorkings()).SelectMany(x => x);

            return base.OptionWorkings;
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
