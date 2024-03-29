using Godot;
using System;

public partial class TerrainPawnView : ViewControl, IItemView
{
    public ProgressWorkingPawnView ProgressWorkingPawn => GetNode<ProgressWorkingPawnView>("VBoxContainer/WorkingPawnContent/ProgressWorkingPawn");
    public ProductWorkingPawnView ProductWorkingPawn => GetNode<ProductWorkingPawnView>("VBoxContainer/WorkingPawnContent/ProductWorkingPawn");

    public ItemContainer<ResourcePawnView> ResourcePawns { get; set; }

    private object id;

    public object Id
    {
        get => id;
        set
        {
            id = value;

            ProgressWorkingPawn.Id = value;
            ProductWorkingPawn.Id = value;
        }
    }

    public (int x, int y) TerrainPosition
    {
        get
        {
            return (((int, int))Id);
        }
    }

    public override void _Ready()
    {
        base._Ready();

        ResourcePawns = new ItemContainer<ResourcePawnView>(() =>
        {
            return GetNode<InstancePlaceholder>("VBoxContainer/HFlowContainer/ResourcePawn");
        });
    }
}