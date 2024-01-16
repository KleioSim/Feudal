using System.Collections.Generic;

namespace Feudal.Godot.Presents;

internal class SessionMock : ISession
{
    public IReadOnlyDictionary<object, ITask> Tasks => MockTasks;

    public Dictionary<object, ITask> MockTasks { get; } = new Dictionary<object, ITask>();

    public IClan PlayerClan { get; set; }

    public IReadOnlyDictionary<object, IResource> Resources => MockResources;

    public Dictionary<object, IResource> MockResources { get; } = new Dictionary<object, IResource>();

    public IReadOnlyDictionary<(int x, int y), ITerrain> Terrains => MockTerrains;

    public IReadOnlyDictionary<object, IWorkHood> WorkHoods => MockWorkHoods;

    public Dictionary<object, IWorkHood> MockWorkHoods { get; } = new Dictionary<object, IWorkHood>();

    public IReadOnlyDictionary<object, IWorking> Workings => MockWorkings;

    public Dictionary<object, IWorking> MockWorkings { get; } = new Dictionary<object, IWorking>();

    public Dictionary<(int x, int y), ITerrain> MockTerrains { get; } = new Dictionary<(int x, int y), ITerrain>();

    public IReadOnlyDictionary<object, IClan> Clans => MockClans;

    public IDate Date => MockDate;

    public Dictionary<object, IClan> MockClans = new Dictionary<object, IClan>();

    private MockDate MockDate { get; } = new MockDate();
}

internal class MockDate : IDate
{
    public int Year { get; set; } = 1;

    public int Month { get; set; } = 1;

    public int Day { get; set; } = 1;
}