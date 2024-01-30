using Feudal.Interfaces;

namespace Feudal.Sessions;

partial class Session
{
    class Finder : IFinder
    {
        public Func<(int x, int y), ITerrain> FindTerrain { get; internal set; }

        public Func<(int x, int y), IWorkHood> FindWorkHoodByPos { get; internal set; }

        public Func<string, IWorking> FindWorking { get; internal set; }

        public Func<string, IWorkHood> FindWorkHood { get; internal set; }

        public Finder(Session session)
        {
            FindWorking = (name) => session.workings[name];

            FindTerrain = (position) => session.Terrains[position];

            FindWorkHoodByPos = (position) =>
            {
                var workHood = session.workHoods.Values.OfType<ITerrainWorkHood>().SingleOrDefault(x => x.Position == position);
                if (workHood == null)
                {
                    workHood = new TerrainWorkHood(position);
                    session.workHoods.Add(workHood.Id, workHood);
                }

                return workHood;
            };

            FindWorkHood = (id) => session.workHoods[id];
        }
    }
}
