using Feudal.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
}