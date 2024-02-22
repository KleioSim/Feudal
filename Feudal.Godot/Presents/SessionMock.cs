using Feudal.Commands;
using Feudal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

    public Dictionary<(int x, int y), ITerrain> MockTerrains { get; } = new Dictionary<(int x, int y), ITerrain>();

    public IReadOnlyDictionary<object, IClan> Clans => MockClans;

    public IDate Date => MockDate;

    public Dictionary<object, IClan> MockClans = new Dictionary<object, IClan>();

    private MockDate MockDate { get; } = new MockDate();

    public void OnCommand(ICommand command)
    {
    }

    internal MockWorkHood GenerateWorkHood()
    {
        var workHood = new MockWorkHood();
        MockWorkHoods.Add(workHood.Id, workHood);
        return workHood;
    }

    internal MockProgressWorking GenerateProgressWorking(MockWorkHood workHood)
    {
        var working = new MockProgressWorking(workHood);
        workHood.MockOptionWorkings.Add(working);

        return working;
    }

    internal MockProductWorking GenerateProductWorking(MockWorkHood workHood)
    {
        var working = new MockProductWorking(workHood);
        workHood.MockOptionWorkings.Add(working);

        return working;
    }

    internal ClanMock GenerateClan(int popCount = 0)
    {
        var clan = new ClanMock();
        clan.PopCount = popCount;

        MockClans.Add(clan.Id, clan);
        return clan;
    }

    internal TaskMock GenerateTask()
    {
        var task = new TaskMock();
        MockTasks.Add(task.Id, task);
        return task;
    }

    internal MockTerrain GenerateTerrain((int, int) position, TerrainType terrainType)
    {
        var terrain = new MockTerrain();
        terrain.Position = position;
        terrain.TerrainType = TerrainType.Plain;

        MockTerrains.Add(terrain.Position, terrain);

        return terrain;
    }

    internal MockResource GenerateTerrainResource(MockTerrain terrain)
    {
        var resource = new MockResource();
        terrain.MockResources.Add(resource);
        MockResources.Add(resource.Id, resource);

        return resource;
    }

    internal IEnumerable<ClanMock> GenerateClans(int count)
    {
        return Enumerable.Range(0, count).Select(x => GenerateClan((x + 1) * 100));
    }
}

internal class MockDate : IDate
{
    public int Year { get; set; } = 1;

    public int Month { get; set; } = 1;

    public int Day { get; set; } = 1;

    public void OnDaysInc()
    {

    }
}