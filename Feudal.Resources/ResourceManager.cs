using Feudal.Interfaces;

namespace Feudal.Resources;

public partial class ResourceManager
{
    public static IFinder Finder { get; set; }

    public ResourceManager()
    {
        IResource resource = new FatSoil();
        dict.Add(resource.Id, resource);
        resource = new Deer();
        dict.Add(resource.Id, resource);
    }

    public IEnumerable<IResource> GetResourcesByTerrainType(TerrainType terrainType)
    {
        var result = new List<IResource>();

        if (terrainType == TerrainType.Plain)
        {
            result.Add(dict[nameof(FatSoil)]);
            result.Add(dict[nameof(Deer)]);
        }

        return result;
    }
}
