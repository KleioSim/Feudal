using Feudal.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Feudal.Sessions;

internal class Session : ISession
{
    public IClan PlayerClan { get; set; }

    public IDate Date { get; init; }

    public IReadOnlyDictionary<object, ITask> Tasks => tasks;

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

    public void OnCommand(Command command, string[] parameters)
    {
        switch (command)
        {
            case Command.NextTurn:
                Date.OnDaysInc();
                break;
            case Command.OccupyLabor:
                {
                    var clanId = parameters[0];
                    var workHooId = parameters[1];

                    var task = tasks.Values.SingleOrDefault(x => x.WorkHoodId == workHooId);
                    if (task != null)
                    {
                        throw new Exception($"WorkHood{workHooId}已经关联Labor{task.ClanId}");
                    }

                    task = new Task(clanId, workHooId);
                    tasks.Add(task.Id, task);
                }
                break;
            case Command.ReleaseLabor:
                {
                    var clanId = parameters[0];
                    var workHooId = parameters[1];

                    var task = tasks.Values.SingleOrDefault(x => x.WorkHoodId == workHooId);
                    if (task == null)
                    {
                        throw new Exception($"WorkHood{workHooId}未关联Labor");
                    }
                    if (task.ClanId != clanId)
                    {
                        throw new Exception($"Task{task.Id}中, WorkHood{workHooId}关联Labor是{task.ClanId}, 不是{clanId}");
                    }

                    tasks.Remove(task.Id);
                }
                break;
        }
    }
}

internal class Task : ITask
{
    public static int Count;

    public string Id { get; }

    public string Desc { get; }

    public float Percent { get; set; }

    public string WorkHoodId { get; }

    public string ClanId { get; private set; }

    public Task(string clanId, string workHoodId)
    {
        Id = $"TASK{Count++}";

        Desc = Id;
        WorkHoodId = workHoodId;
        ClanId = clanId;
    }
}

internal class DiscoverWorking : IWorking
{
    public string Id { get; } = nameof(DiscoverWorking);

    public string Name => Id;
}

internal class WorkHood : IWorkHood
{
    public static int Count;

    public string Id { get; }

    public IWorking CurrentWorking { get; private set; }

    public IEnumerable<IWorking> OptionWorkings { get; private set; }

    internal void UpdateWorkings(IEnumerable<IWorking> workings)
    {
        OptionWorkings = workings;

        if (OptionWorkings.Contains(CurrentWorking))
        {
            return;
        }

        CurrentWorking = OptionWorkings.First();
    }

    public WorkHood()
    {
        Id = $"WorkHood_{Count++}";
    }
}

internal class Terrain : ITerrain
{
    public delegate void DelegateOccupyOrUpdateWorkHood(ref string Id, IEnumerable<IWorking> workings);

    public static DelegateOccupyOrUpdateWorkHood OccupyOrUpdateWorkHood;
    public static Action<string> ReleaseWorkHood;

    public static IWorking DiscoverWorking { get; set; }

    public (int x, int y) Position { get; }

    public IReadOnlySet<IResource> Resources => resources;

    public TerrainType TerrainType { get; private set; }

    public bool IsDiscoverd { get; set; }

    public string WorkHoodId
    {
        get
        {
            var workingOptions = GetWorkingOptions();
            if (workingOptions.Any())
            {
                OccupyOrUpdateWorkHood(ref workHoodId, workingOptions);
            }
            else
            {
                ReleaseWorkHood(workHoodId);
                workHoodId = null;
            }

            return workHoodId;
        }
    }

    private string workHoodId;

    private HashSet<IResource> resources = new HashSet<IResource>();

    public Terrain((int x, int y) position, TerrainType terrainType)
    {
        this.Position = position;
        this.TerrainType = terrainType;
    }

    private IEnumerable<IWorking> GetWorkingOptions()
    {
        if (!IsDiscoverd)
        {
            return new[] { DiscoverWorking };
        }

        return Resources.Select(x => x.GetWorkings()).SelectMany(x => x);
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