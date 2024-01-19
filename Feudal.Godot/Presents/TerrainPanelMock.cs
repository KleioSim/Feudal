using Feudal.Godot.UICommands;
using Feudal.Interfaces;
using Godot;
using System;
using System.Linq;

namespace Feudal.Godot.Presents;

[Tool]
partial class TerrainPanelMock : MockControl<TerrainPanelView, ISessionModel>
{
    [Export]
    public bool IsDiscovered
    {
        get
        {
            var terrain = Present.Model.Session.Terrains[(View.TerrainPosition.X, View.TerrainPosition.Y)];
            return terrain.IsDiscoverd;
        }
        set
        {
            var terrain = Present.Model.Session.Terrains[(View.TerrainPosition.X, View.TerrainPosition.Y)] as MockTerrain;
            terrain.IsDiscoverd = value;

            Present.SendCommand(new UICommand_Refresh());
        }
    }

    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            var terrain = new MockTerrain();
            terrain.Position = (0, 0);
            terrain.TerrainType = TerrainType.Plain;
            View.TerrainPosition = new Vector2I(terrain.Position.x, terrain.Position.y);

            session.MockTerrains.Add(terrain.Position, terrain);

            for (int i = 0; i < 3; i++)
            {
                var resource = new MockResource();
                terrain.MockResources.Add(resource);
                session.MockResources.Add(resource.Id, resource);
            }

            var workHood = new MockWorkHood();
            terrain.WorkHoodId = workHood.Id;

            session.MockWorkHoods.Add(workHood.Id, workHood);

            for (int i = 0; i < 2; i++)
            {
                var working = new MockWorking();

                workHood.MockOptionWorkings.Add(working);
                session.MockWorkings.Add(working.Id, working);
            }

            workHood.CurrentWorking = workHood.OptionWorkings.First();

            var clan = new ClanMock();
            session.MockClans.Add(clan.Id, clan);

            var task = new TaskMock();
            task.ClanId = clan.Id;
            task.WorkHoodId = workHood.Id;

            session.MockTasks.Add(task.Id, task);

            return new SessionModel() { Session = session };
        }
    }
}