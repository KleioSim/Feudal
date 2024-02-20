using Godot;

public partial class ProductWorkingPawnView : WorkingPawnView
{
    public Label Label => GetNode<Label>("Panel/VBoxContainer/Label");
    public Label Value => GetNode<Label>("Panel/VBoxContainer/Value");
}