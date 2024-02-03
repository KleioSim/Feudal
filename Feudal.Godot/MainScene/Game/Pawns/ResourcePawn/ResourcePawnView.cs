using Godot;
using System;

public partial class ResourcePawnView : ViewControl, IItemView
{
    public object Id { get; set; }

    public Label Label => GetNode<Label>("Label");
}