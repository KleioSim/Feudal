using Feudal.Interfaces;
using System.Collections.Generic;

namespace Feudal.Godot.Presents;

public partial class ProgressWorkingPawnMock : MockControl<ProgressWorkingPawnView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            var terrain = session.GenerateTerrain((0, 0), TerrainType.Plain);
            View.Id = terrain.Position;

            var working = terrain.GenerateProgressWorking();
            working.Percent = 33;

            return new SessionModel() { Session = session };
        }
    }
}