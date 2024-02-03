using Godot;
using System;
using System.Linq;

public partial class PawnsPanelView : ViewControl
{
    public ItemContainer<WorkingPawnView> WorkingPawns { get; set; }

    public ItemContainer<TerrainPawnView> TerrainPawns { get; set; }

    public Func<Vector2I, Vector2> CalcGlobalPosition { get;  set; }

    public override void _Ready()
    {
        base._Ready();


        TerrainPawns = new ItemContainer<TerrainPawnView>(() =>
        {
            return GetNode<InstancePlaceholder>("TerrainPawn");
        });

        TerrainPawns.OnAddedItem = (item) =>
        {
            var offset = new Vector2(item.Size.X / 2, item.Size.Y*4/5);
            item.Position = CalcGlobalPosition(new Vector2I(item.TerrainPosition.x, item.TerrainPosition.y)) - offset;
        };
    }
}
