using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public partial class ClanPanelView : MainPanelView
{
    public Label Title => GetNode<Label>("VBoxContainer/Title");
    public Label PopCount => GetNode<Label>("VBoxContainer/HBoxContainer/Pop/Value");
    public Label LaborCount => GetNode<Label>("VBoxContainer/HBoxContainer/Labor/Value");

    public string Id { get; set; }
}