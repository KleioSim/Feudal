using Feudal.Interfaces;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Feudal.WorkHoods;

public partial class WorkHoodManager : IReadOnlyDictionary<object, IWorkHood>
{
    private Dictionary<object, IWorkHood> dict = new Dictionary<object, IWorkHood>();

    public static IFinder Finder
    {
        get => TerrainWorkHood.Finder;
        set
        {
            TerrainWorkHood.Finder = value;
        }
    }

    internal IWorkHood AddOrFindTerrainWorkHood((int x, int y) position)
    {
        var workHood = dict.Values.OfType<ITerrainWorkHood>().SingleOrDefault(x => x.Position == position);
        if (workHood == null)
        {
            workHood = new TerrainWorkHood(position);
            dict.Add(workHood.Id, workHood);
        }

        return workHood;
    }
}
