using Feudal.Interfaces;
using Feudal.Workings;

namespace Feudal.Resources;

class FatSoil : IResource
{
    public string Id => Name;

    public string Name => this.GetType().Name;

    public IEnumerable<Type> GetWorkingTypes()
    {
        return new[] { typeof(BuildingFarm) };
    }
}
