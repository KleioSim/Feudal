using Godot;
using System;

public partial class TerrainPanelView : MainPanelView
{
    public Vector2I TerrainPosition { get; set; }

    public Label Title => GetNode<Label>("VBoxContainer/Title");

    public ResourceArrayView ResourceArray => GetNode<ResourceArrayView>("VBoxContainer/PanelContainer/ResourceArray");

    public WorkHoodView WorkHoodPanel => GetNode<WorkHoodView>("VBoxContainer/WorkHoodPanel");
}
