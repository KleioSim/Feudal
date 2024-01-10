using System.Collections.Generic;

namespace Feudal.Godot.Presents;

internal class SessionMock : ISession
{
    public IReadOnlyDictionary<object, ITask> Tasks => MockTasks;

    public Dictionary<object, ITask> MockTasks { get; } = new Dictionary<object, ITask>();

    public IClan PlayerClan { get; } = new ClanMock();

    public IReadOnlyDictionary<object, IResource> Resources => MockResources;

    public Dictionary<object, IResource> MockResources { get; } = new Dictionary<object, IResource>();

    public IReadOnlyDictionary<(int x, int y), ITerrain> Terrains => MockTerrains;

    public IReadOnlyDictionary<object, IWorkHood> WorkHoods => MockWorkHoods;

    public Dictionary<object, IWorkHood> MockWorkHoods { get; } = new Dictionary<object, IWorkHood>();

    public IReadOnlyDictionary<object, IWorking> Workings => MockWorkings;

    public Dictionary<object, IWorking> MockWorkings { get; } = new Dictionary<object, IWorking>();

    public Dictionary<(int x, int y), ITerrain> MockTerrains { get; } = new Dictionary<(int x, int y), ITerrain>();
}
