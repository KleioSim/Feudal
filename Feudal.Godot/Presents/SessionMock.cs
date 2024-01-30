using Feudal.Interfaces;
using System;
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

    public void OnCommand(Command command, string[] parameters)
    {
    }

    internal MockWorkHood GenerateWorkHood()
    {
        var workHood = new MockWorkHood();
        MockWorkHoods.Add(workHood.Id, workHood);
        return workHood;
    }

    internal MockWorking GenerateWorking(MockWorkHood workHood = null)
    {
        var working = new MockWorking();

        MockWorkings.Add(working.Id, working);

        if (workHood != null)
        {
            workHood.MockOptionWorkings.Add(working);
            workHood.CurrentWorking = working;
        }

        return working;
    }

    internal ClanMock GenerateClan()
    {
        var clan = new ClanMock();
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
        terrain.Position = (0, 0);
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

    internal MockTerrainWorkHood GenerateTerrainWorkHood(MockTerrain terrain)
    {
        var workHood = new MockTerrainWorkHood();
        terrain.WorkHood = workHood;
        workHood.Position = terrain.Position;

        MockWorkHoods.Add(workHood.Id, workHood);

        return workHood;
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