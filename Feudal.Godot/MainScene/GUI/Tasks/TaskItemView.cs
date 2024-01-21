using Godot;
using System;

public partial class TaskItemView : ViewControl, IItemView
{
    public Label Label => GetNode<Label>("VBoxContainer/Label");
    public ProgressBar Progress => GetNode<ProgressBar>("VBoxContainer/ProgressBar");
    public Button Button => GetNode<Button>("Button");
    public object Id { get; set; }
}