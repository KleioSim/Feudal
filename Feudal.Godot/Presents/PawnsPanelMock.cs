using Feudal.Interfaces;
using Godot;
using System.Collections.Generic;

namespace Feudal.Godot.Presents;

partial class PawnsPanelMock : MockControl<PawnsPanelView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            for (int i = 0; i < 3; i++)
            {

                var terrain = session.GenerateTerrain((i, i), TerrainType.Plain);

                var task = session.GenerateTask();
                task.Working = terrain.GenerateProgressWorking();
            }

            return new SessionModel() { Session = session };
        }
    }
}