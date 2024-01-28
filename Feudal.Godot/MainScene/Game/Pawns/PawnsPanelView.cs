using Godot;
using System;
using System.Linq;

public partial class PawnsPanelView : ViewControl
{
    public ItemContainer<WorkingPawnView> WorkingPawns { get; set; }
    public Func<Vector2I, Vector2> CalcGlobalPosition { get;  set; }

    public override void _Ready()
    {
        base._Ready();

        WorkingPawns = new ItemContainer<WorkingPawnView>(() =>
        {
            return GetNode<InstancePlaceholder>("WorkingPawn");
        });

        WorkingPawns.OnAddedItem = (item) =>
        {
            item.Position = CalcGlobalPosition(new Vector2I(item.TerrainPosition.x, item.TerrainPosition.y));
        };
    }
}
