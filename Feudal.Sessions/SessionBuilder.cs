using Feudal.Interfaces;
using Feudal.TerrainBuilders;

namespace Feudal.Sessions;

public class SessionBuilder
{
    public static ISession BuildDebug()
    {
        var session = new Session()
        {
            Date = new Date(),
        };

        for (int i = 0; i < 3; i++)
        {
            var clan = new Clan($"clan{i}", i * 1000);
            session.clans.Add(clan.Id, clan);
        }

        session.PlayerClan = session.clans.Values.First();

        var terrainBuilder = new TerrainBuilder(TerrainType.Hill);
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                var pos = (x, y);
                var terrain = new Terrain(pos, terrainBuilder.Build(pos));
                if (pos == (0, 0))
                {
                    terrain.IsDiscoverd = true;
                }

                session.terrains.Add(terrain.Position, terrain);
            }
        }

        var discoverWorking = new DiscoverWorking();
        session.workings.Add(discoverWorking.Id, discoverWorking);

        Terrain.DiscoverWorking = discoverWorking;

        Terrain.OccupyOrUpdateWorkHood = (ref string id, IEnumerable<IWorking> workings) =>
        {
            if (id == null || !session.workHoods.TryGetValue(id, out var workHood))
            {
                workHood = new WorkHood();
                session.workHoods.Add(workHood.Id, workHood);
            }

            ((WorkHood)workHood).UpdateWorkings(workings);

            id = workHood.Id;
        };

        Terrain.ReleaseWorkHood = (string id) =>
        {
            if (id != null)
            {
                session.workHoods.Remove(id);
            }
        };


        return session;
    }
}