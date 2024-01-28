using Godot;
using System;

public partial class WorkingPawnView : ViewControl, IItemView
{
    public object Id { get; set; }

    public (int x, int y) TerrainPosition
    {
        get
        {
            return (((int, int)) Id);
        }
    }
}
