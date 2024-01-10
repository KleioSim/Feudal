using Godot;
using System;

public partial class ClanItemView : ViewControl, IItemView
{
    public static Action<string> ShowClan;

    public Label Label => GetNode<Label>("Button/HBoxContainer/Label");
    public Label Type => GetNode<Label>("Button/HBoxContainer/Type");
    public Label PopCount => GetNode<Label>("Button/HBoxContainer/Pop");

    public Button Button => GetNode<Button>("Button");

    public  object Id { get; set; }

    public override void _Ready()
    {
        Button.Pressed += () => ShowClan?.Invoke(Id as string);

        base._Ready();
    }
}
