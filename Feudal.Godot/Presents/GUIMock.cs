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
            var clans = session.GenerateClans(3);
            foreach (var clan in clans)
            {
                clan.GenerateLabors(3);
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

            session.GenerateProductWorking(workHood);
            session.GenerateProgressWorking(workHood);

            workHood.CurrentWorking = workHood.OptionWorkings.First();

            return new SessionModel() { Session = session };
        }
    }
}