using Godot;

public partial class LaborItemView : ViewControl, IItemView
{
    public object Id { get; set; }

    public Label ClanName => GetNode<Label>("HBoxContainer/Label");

    public Label IdleCount => GetNode<Label>("HBoxContainer/HBoxContainer/IdleCount");
    public Label TotalCount => GetNode<Label>("HBoxContainer/HBoxContainer/TotalCount");

    public Button Button => GetNode<Button>("Button");
}