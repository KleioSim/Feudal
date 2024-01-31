using Feudal.Interfaces;

namespace Feudal.Resources;

public partial class ResourceManager
{
    public static IFinder Finder { get; set; }

    public ResourceManager()
    {
        var resource = new FatSoil();
        dict.Add(resource.Id, resource);
    }

    public IEnumerable<IResource> GetResourcesByTerrainType(TerrainType terrainType)
    {
        var result = new List<IResource>();

        if (terrainType == TerrainType.Plain)
        {
            result.Add(dict[nameof(FatSoil)]);
        }

        return result;
    }
}
