using Feudal.Interfaces;

namespace Feudal.Terrains;

class Terrain : ITerrain
{
    public static Func<ITerrain, string> GetWorkHoodId;

    public (int x, int y) Position { get; }

    public IReadOnlySet<IResource> Resources => resources;

    public TerrainType TerrainType { get; private set; }

    public bool IsDiscoverd { get; set; }

    public string WorkHoodId => GetWorkHoodId(this);

    private HashSet<IResource> resources = new HashSet<IResource>();

    public Terrain((int x, int y) position, TerrainType terrainType)
    {
        this.Position = position;
        this.TerrainType = terrainType;
    }
}