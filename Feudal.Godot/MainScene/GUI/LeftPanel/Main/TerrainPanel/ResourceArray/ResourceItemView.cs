using Godot;

public partial class ResourceItemView : ViewControl, IItemView
{
    public object Id { get; set; }

    public Label Label => GetNode<Label>("Label");
}