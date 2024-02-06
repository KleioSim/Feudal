using Feudal.Interfaces;

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
            var workings = Finder.FindWorkingsInTerrain(Position);
            UpdateWorkings(workings);

            return base.OptionWorkings;
        }
    }
}