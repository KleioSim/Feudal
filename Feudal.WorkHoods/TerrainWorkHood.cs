using Feudal.Interfaces;
using Feudal.WorkHoods.Workings;

namespace Feudal.WorkHoods;

class TerrainWorkHood : WorkHood, ITerrainWorkHood
{
    public static IFinder Finder { get; set; }

    public (int x, int y) Position { get; }

    public TerrainWorkHood((int x, int y) position)
    {
        this.Position = position;
    }

    public override IEnumerable<IWorking> OptionWorkings
    {
        get
        {
            var workingTypes = FindWorkingsInTerrain(Position);
            UpdateWorkings(workingTypes);

            return base.OptionWorkings;
        }
    }

    private IEnumerable<Type> FindWorkingsInTerrain((int x, int y) position)
    {
        var terrain = Finder.FindTerrain(position);
        if (!terrain.IsDiscoverd)
        {
            return new[] { typeof(DiscoverWorking) };
        }

        return new Type[] { };
    }
}