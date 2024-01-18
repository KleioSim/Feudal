using Feudal.Godot.Presents;
using Feudal.Interfaces;
using Feudal.TerrainBuilders;
using Godot;
using System;
using System.Linq;

public partial class DebugMapBuilderPresent : PresentControl<DebugMapBuilderView, ISessionModel>
{
    private (int x, int y) currPos;
    private TerrainBuilder terrainBuilder;

    private (int x, int y) GetNextPosition((int x, int y) Position)
    {
        if (Position.x == 0 && Position.y == 0)
        {
            return (0, 1);
        }

        if (Position.x == 1 && Position.y > 0)
        {
            return (0, Position.y + 1);
        }

        if (Math.Abs(Position.x) < Math.Abs(Position.y))
        {
            if (Position.y > 0)
            {
                return (Position.x - 1, Position.y);
            }
            else
            {
                return (Position.x + 1, Position.y);
            }
        }

        if (Math.Abs(Position.y) < Math.Abs(Position.x))
        {
            if (Position.x > 0)
            {
                return (Position.x, Position.y + 1);
            }
            else
            {
                return (Position.x, Position.y - 1);
            }
        }

        if (Position.x < 0 && Position.y > 0)
        {
            return (Position.x, Position.y - 1);
        }
        if (Position.x < 0 && Position.y < 0)
        {
            return (Position.x + 1, Position.y);
        }
        if (Position.x > 0 && Position.y < 0)
        {
            return (Position.x, Position.y + 1);
        }
        if (Position.x > 0 && Position.y > 0)
        {
            return (Position.x - 1, Position.y);
        }

        throw new Exception();
    }


    protected override void Initialize(DebugMapBuilderView view, ISessionModel model)
    {
        var mockSession = model.Session as SessionMock;

        view.Generate.Pressed += () =>
        {
            mockSession.MockTerrains.Clear();

            var mapType = Enum.Parse<TerrainType>(view.SelectedTerrainType);
            terrainBuilder = new TerrainBuilder(mapType);

            currPos = (0, 0);

            view.Timer.Start();
        };

        view.Timer.Timeout += () =>
        {
            if (Math.Abs(currPos.y) >= view.MapSize || Math.Abs(currPos.x) >= view.MapSize)
            {
                view.Timer.Stop();
                return;
            }


            var terrainType = terrainBuilder.Build(currPos);

            mockSession.MockTerrains.Add(currPos, new MockTerrain() { Position = currPos, TerrainType = terrainType });

            currPos = GetNextPosition(currPos);

            SendCommand(new UIRefreshCommand());
        };

        view.TilemapView.ClickTile += (Vector2I index) =>
        {
            var pos = (index.X, index.Y);
            if (mockSession.MockTerrains.ContainsKey(pos))
            {
                return;
            }

            var terrainType = terrainBuilder.Build(currPos);

            mockSession.MockTerrains.Add(currPos, new MockTerrain() { Position = pos, TerrainType = terrainType });

            SendCommand(new UIRefreshCommand());
        };
    }

    protected override void Update(DebugMapBuilderView view, ISessionModel model)
    {

    }
}