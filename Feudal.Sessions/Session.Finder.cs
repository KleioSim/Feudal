using Feudal.Interfaces;
using Feudal.WorkHoods;

namespace Feudal.Sessions;

partial class Session
{
    class Finder : IFinder
    {
        public Func<(int x, int y), ITerrain> FindTerrain { get; internal set; }

        public Func<(int x, int y), IWorkHood> FindWorkHoodByPos { get; internal set; }

        public Func<string, IWorking> FindWorking { get; internal set; }

        public Func<string, IWorkHood> FindWorkHood { get; internal set; }

        public Func<(int x, int y), IEnumerable<IWorking>> FindWorkingsInTerrain { get; internal set; }

        public Func<TerrainType, IEnumerable<IResource>> FindResourceByTerrainType { get; internal set; }

        public Func<string, IClan> FindClan { get; internal set; }

        public Func<string, ILabor> FindLabor { get; internal set; }

        public Func<ILabor, ITask> FindTaskByLabor { get; internal set; }

        public Finder(Session session)
        {
            FindWorking = (id) => session.workHoodManager.FindWorking(id);

            FindTerrain = (position) => session.Terrains[position];

            FindWorkHoodByPos = (position) => session.workHoodManager.AddOrFindTerrainWorkHood(position);

            FindWorkHood = (id) => session.WorkHoods[id];

            FindResourceByTerrainType = (terrainType) => session.resourceManager.GetResourcesByTerrainType(terrainType);

            FindClan = (id) => session.clanManager[id];

            FindLabor = (id) => session.clanManager.FindLabor(id);

            FindTaskByLabor = (labor) => session.Tasks.Values.SingleOrDefault(x => x.Labor == labor);
        }
    }
}
