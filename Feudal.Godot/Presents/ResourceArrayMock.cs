using Feudal.Interfaces;
using System;
using System.Collections.Generic;

namespace Feudal.Godot.Presents;

partial class ResourceArrayMock : MockControl<ResourceArrayView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var terrain = new MockTerrain();
            terrain.Position = (0, 0);
            View.TerrainPos = terrain.Position;

            var session = new SessionMock();

            for (int i = 0; i < 3; i++)
            {
                var resource = new MockResource();
                terrain.MockResources.Add(resource);
                session.MockResources.Add(resource.Id, resource);
            }

            session.MockTerrains.Add(terrain.Position, terrain);


            return new SessionModel() { Session = session };
        }
    }
}

class MockTerrain : ITerrain, IWorkHood
{
    public (int x, int y) Position { get; set; }

    public IReadOnlySet<IResource> Resources => MockResources;

    public TerrainType TerrainType { get; set; }

    public bool IsDiscoverd { get; set; } = false;

    public IWorkHood WorkHood { get; set; }

    public string Id => throw new System.NotImplementedException();

    public IWorking CurrentWorking => throw new System.NotImplementedException();

    public IEnumerable<IWorking> OptionWorkings => optionWorkings;

    public HashSet<IResource> MockResources = new HashSet<IResource>();

    private List<IWorking> optionWorkings = new List<IWorking>();

    internal MockProgressWorking GenerateProgressWorking()
    {
        var working = new MockProgressWorking(this);
        optionWorkings.Add(working);
        return working;
    }

    internal MockProductWorking GenerateProductWorking()
    {
        var working = new MockProductWorking(this);
        optionWorkings.Add(working);
        return working;
    }
}
