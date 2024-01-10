using Godot;
using System;

public partial class TilemapView : ViewControl
{
    [Signal]
    public delegate void ClickTileEventHandler(Vector2I index);

    public TileMap Tilemap => GetNode<TileMap>("TileMap");
    public Camera2D Camera => GetNode<Camera2D>("Camera2D");

    private Vector2 zoomStep = new Vector2(0.02f, 0.02f);
    private Vector2 maxZoom = new Vector2(0.8f, 0.8f);
    private Vector2 minZoom = new Vector2(0.1f, 0.1f);

    public override void _Ready()
    {
        base._Ready();
        Camera.Position = Tilemap.MapToLocal(new Vector2I(0, 0));
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventKey)
        {
            if (eventKey.Pressed)
            {
                if (eventKey.ButtonIndex == MouseButton.Left)
                {
                    var tileIndex = Tilemap.LocalToMap(GetGlobalMousePosition());
                    GD.Print(tileIndex);

                    if (Tilemap.GetCellSourceId(0, tileIndex) != -1)
                    {
                        EmitSignal(SignalName.ClickTile, tileIndex);
                    }
                }
                else if (eventKey.ButtonIndex == MouseButton.WheelDown)
                {
                    if (Camera.Zoom + zoomStep <= maxZoom)
                    {
                        Camera.Zoom += zoomStep;
                    }
                }
                else if (eventKey.ButtonIndex == MouseButton.WheelUp)
                {
                    if (Camera.Zoom - zoomStep >= minZoom)
                    {
                        Camera.Zoom -= zoomStep;
                    }
                }
            }
            return;
        }
    }
}
