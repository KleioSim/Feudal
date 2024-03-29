﻿using Feudal.Clans;
using Feudal.Interfaces;

namespace Feudal.Sessions;

public class SessionBuilder
{
    public static ISession BuildDebug()
    {
        var session = new Session()
        {
            Date = new Date(),
        };

        session.clanManager.Initialize();
        session.PlayerClan = session.Clans.Values.First();
        session.terrainManager.Initialize(TerrainType.Hill);


        //var discoverWorking = new DiscoverWorking(session);
        //session.workings.Add(discoverWorking.Id, discoverWorking);

        //TerrainManager.GetWorkHoodId = (terrain) =>
        //{
        //    var workings = !terrain.IsDiscoverd ? new[] { session.workings[typeof(DiscoverWorking).Name] } : terrain.Resources.Select(x => x.GetWorkings()).SelectMany(x => x).ToArray();
        //    var workHood = session.workHoods.Values.OfType<ITerrainWorkHood>().SingleOrDefault(x => x.Position == terrain.Position);
        //    if (workings.Any())
        //    {
        //        if (workHood == null)
        //        {
        //            workHood = new TerrainWorkHood(terrain.Position);
        //            session.workHoods.Add(workHood.Id, workHood);
        //        }
        //        ((WorkHood)workHood).UpdateWorkings(workings);
        //        return workHood.Id;
        //    }
        //    else
        //    {
        //        if (workHood != null) { session.workHoods.Remove(workHood.Id); }
        //        return null;
        //    }
        //};

        //TaskManager.FindWorkHood = (id) =>
        //{
        //    return session.WorkHoods[id];
        //};

        //Terrain.DiscoverWorking = discoverWorking;

        //Terrain.OccupyOrUpdateWorkHood = (ref string id, IEnumerable<IWorking> workings) =>
        //{
        //    if (id == null || !session.workHoods.TryGetValue(id, out var workHood))
        //    {
        //        workHood = new WorkHood();
        //        session.workHoods.Add(workHood.Id, workHood);
        //    }

        //    ((WorkHood)workHood).UpdateWorkings(workings);

        //    id = workHood.Id;
        //};

        //Terrain.ReleaseWorkHood = (string id) =>
        //{
        //    if (id != null)
        //    {
        //        session.workHoods.Remove(id);
        //    }
        //};


        return session;
    }
}