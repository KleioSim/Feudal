using Feudal.Interfaces;

namespace Feudal.Resources;

class FatSoil : IResource
{
    public string Id => Name;

    public string Name => this.GetType().Name;

    public IEnumerable<string> GetWorkings()
    {
        return new[] { "BuildingFarm" };
    }
}
