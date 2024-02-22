using Feudal.Interfaces;

namespace Feudal.Terrains;

class Terrain : ITerrain
{
    public static IFinder Finder { get; set; }

    public (int x, int y) Position { get; }

    public IReadOnlySet<IResource> Resources => resources;

    public TerrainType TerrainType { get; private set; }

    public bool IsDiscoverd { get; set; }

    public IEnumerable<IWorking> OptionWorkings
    {
        get
        {
            var workingChanges = Finder.FindWorkingChanges(this);

            var needRemoves = workingChanges.Where(change => change.ChangeType == ItemChangeType.Remove)
                .Select(x => x.Item);

            var needAdds = workingChanges.Where(change => change.ChangeType == ItemChangeType.Add)
                .Select(x => x.Item);

            optionWorkings.RemoveAll(x => needRemoves.Contains(x));
            optionWorkings.AddRange(needAdds);

            return optionWorkings;
        }
    }

    private List<IWorking> optionWorkings = new List<IWorking>();

    private HashSet<IResource> resources = new HashSet<IResource>();

    public Terrain((int x, int y) position, TerrainType terrainType)
    {
        this.Position = position;
        this.TerrainType = terrainType;

        foreach (var resource in Finder.FindResourceByTerrainType(TerrainType))
        {
            resources.Add(resource);
        }
    }
}