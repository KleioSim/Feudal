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
            var terrain = Finder.FindTerrain(Position);

            var workings = Finder.FindWorkingsInTerrain(Position);
            foreach (var working in workings)
            {
                working.WorkHood = this;
            }

            base.OptionWorkings = workings;

            return base.OptionWorkings;
        }
    }
}