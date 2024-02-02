using Godot;
using System.Linq;
using System;
using Feudal.Interfaces;

namespace Feudal.Godot.Presents;

partial class GUIMock : MockControl<GUIView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();
            for (int i = 0; i < 3; i++)
            {
                var clan = new ClanMock();
                clan.LaborMock.TotalCount = i * 10;
                session.MockClans.Add(clan.Id, clan);
            }
            session.PlayerClan = session.Clans.Last().Value;

            View.PlayerClanId = session.PlayerClan.Id;

            var terrain = new MockTerrain();
            terrain.Position = (0, 0);
            terrain.TerrainType = TerrainType.Plain;
            session.MockTerrains.Add(terrain.Position, terrain);

            for (int i = 0; i < 3; i++)
            {
                var resource = new MockResource();
                terrain.MockResources.Add(resource);
                session.MockResources.Add(resource.Id, resource);
            }

            var workHood = new MockWorkHood();
            terrain.WorkHood = workHood;

            session.MockWorkHoods.Add(workHood.Id, workHood);

            for (int i = 0; i < 2; i++)
            {
                var working = new MockProgressWorking();

                workHood.MockOptionWorkings.Add(working);
                session.MockWorkings.Add(working.Id, working);
            }

            workHood.CurrentWorking = workHood.OptionWorkings.First();

            return new SessionModel() { Session = session };
        }
    }
}