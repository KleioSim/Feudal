using Godot;
using System;

public partial class TerrainPanelView : MainPanelView
{
    public Vector2I TerrainPosition { get; set; }

    public Label Title => GetNode<Label>("VBoxContainer/Title");

    public Control ResourceArray => GetNode<Control>("VBoxContainer/ResourceArray");

    public WorkHoodView WorkHoodPanel => GetNode<WorkHoodView>("VBoxContainer/WorkHoodPanel");
}
