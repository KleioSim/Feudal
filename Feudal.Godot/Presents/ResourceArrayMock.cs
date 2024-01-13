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


            return new ModelMock() { Session = session };
        }
    }
}

class MockTerrain : ITerrain
{
    public (int x, int y) Position { get; set; }

    public IReadOnlySet<IResource> Resources => MockResources;

    public TerrainType TerrainType { get; set; }

    public bool IsDiscoverd { get; set; } = false;

    public string WorkHoodId { get; set; }

    public HashSet<IResource> MockResources = new HashSet<IResource>();
}
