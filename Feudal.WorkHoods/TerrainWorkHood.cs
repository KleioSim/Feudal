//using Feudal.Interfaces;
//using Feudal.WorkHoods.Workings;
//using System.Reflection;

//namespace Feudal.WorkHoods;

//class TerrainWorkHood : WorkHood, ITerrainWorkHood
//{
//    public static Dictionary<string, Type> workingTypes { get; } = Assembly.GetExecutingAssembly().GetTypes().ToDictionary(x => x.Name, x => x);

//    public static IFinder Finder { get; set; }

//    public (int x, int y) Position { get; }

//    public TerrainWorkHood((int x, int y) position)
//    {
//        this.Position = position;
//    }

//    public override IEnumerable<IWorking> OptionWorkings
//    {
//        get
//        {
//            var workingTypes = FindWorkingsInTerrain(Position);
//            UpdateWorkings(workingTypes);

//            return base.OptionWorkings;
//        }
//    }

//    private IEnumerable<Type> FindWorkingsInTerrain((int x, int y) position)
//    {
//        var terrain = Finder.FindTerrain(position);
//        if (!terrain.IsDiscoverd)
//        {
//            return new[] { typeof(DiscoverTerrain) };
//        }

//        return terrain.Resources.SelectMany(x => x.GetWorkings()).Select(x => workingTypes[x]);
//    }
//}