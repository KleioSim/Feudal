using Feudal.Clans;
using Feudal.Commands;
using Feudal.Interfaces;
using Feudal.Resources;
using Feudal.Tasks;
using Feudal.Terrains;
using Feudal.WorkHoods;

namespace Feudal.Sessions;

partial class Session : ISession
{
    public IClan PlayerClan { get; set; }

    public IDate Date { get; init; }

    public IReadOnlyDictionary<object, ITask> Tasks => taskManager;

    public IReadOnlyDictionary<object, IResource> Resources => resourceManager;

    public IReadOnlyDictionary<(int x, int y), ITerrain> Terrains => terrainManager;

    public IReadOnlyDictionary<object, IWorkHood> WorkHoods => workHoodManager;

    public IReadOnlyDictionary<object, IClan> Clans => clans;


    internal readonly Dictionary<object, IClan> clans = new Dictionary<object, IClan>();

    internal readonly TaskManager taskManager = new TaskManager();
    internal readonly TerrainManager terrainManager = new TerrainManager();
    internal readonly WorkHoodManager workHoodManager = new WorkHoodManager();
    internal readonly ResourceManager resourceManager = new ResourceManager();

    public Session()
    {

        var finder = new Finder(this);

        WorkHoodManager.Finder = finder;
        TerrainManager.Finder = finder;
        TaskManager.Finder = finder;
        ResourceManager.Finder = finder;

        TaskManager.CommandSender = OnCommand;

        TaskManager.OnTaskAdded = (task) =>
        {
            if (task.Working is IProductWorking productWorking)
            {
                var clan = PlayerClan as Clan;

                clan.AddProductTask(task);
            }
        };

        TaskManager.OnTaskRemoved = (task) =>
        {
            if (task.Working is IProductWorking productWorking)
            {
                var clan = PlayerClan as Clan;

                clan.RemoveProductTask(task);
            }
        };
    }

    public void OnCommand(ICommand command)
    {
        switch (command)
        {
            case Command_NextTurn:
                {
                    taskManager.OnNextTurn();
                    Date.OnDaysInc();
                }
                break;
            case Command_OccupyLabor cmdOccupyLabor:
                {
                    taskManager.CreateTask(cmdOccupyLabor.ClanId, cmdOccupyLabor.WorkingId);
                }
                break;
            case Command_ReleaseLabor cmdReleaseLabor:
                {
                    taskManager.RelaseTask(cmdReleaseLabor.ClanId, cmdReleaseLabor.WorkingId);
                }
                break;
            case Command_DiscoverTerrain cmdDiscoverTerrain:
                {
                    terrainManager.SetDiscoverd((int.Parse(cmdDiscoverTerrain.Position_X), int.Parse(cmdDiscoverTerrain.Position_Y)));
                }
                break;
        }
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
