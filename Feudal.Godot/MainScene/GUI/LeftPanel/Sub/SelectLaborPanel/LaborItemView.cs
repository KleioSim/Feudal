using Godot;

public partial class LaborItemView : ViewControl, IItemView
{
    public object Id { get; set; }

    public Label ClanName => GetNode<Label>("HBoxContainer/Label");
    public Label Count => GetNode<Label>("HBoxContainer/Pop");

    public Button Button => GetNode<Button>(".");
}