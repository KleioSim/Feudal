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

            var terrain = session.GenerateTerrain((0, 0), TerrainType.Plain);
            View.TerrainPosition = new Vector2I(terrain.Position.x, terrain.Position.y);

            for (int i = 0; i < 3; i++)
            {
                session.GenerateTerrainResource(terrain);
            }

            var workHood = session.GenerateTerrainWorkHood(terrain);

            for (int i = 0; i < 3; i++)
            {
                session.GenerateWorking(workHood);
            }

            var task = session.GenerateTask();
            task.ClanId = session.GenerateClan().Id;
            task.WorkHoodId = workHood.Id;

            return new SessionModel() { Session = session };
        }
    }
}