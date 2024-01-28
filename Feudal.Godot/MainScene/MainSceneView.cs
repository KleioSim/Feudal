using Godot;
using System;

public partial class MainSceneView : Control
{
    public TilemapView TilemapView => GetNode<TilemapView>("MapLayer/Map");
    public PawnsPanelView PawnsPanel => GetNode<PawnsPanelView>("MapLayer/PawnsPanel");

    public override void _Ready()
    {
        PawnsPanel.CalcGlobalPosition = (TerrainPosition) =>
        {
            var local = TilemapView.Tilemap.MapToLocal(TerrainPosition);
            return TilemapView.Tilemap.ToGlobal(local);
        };
    }
}
