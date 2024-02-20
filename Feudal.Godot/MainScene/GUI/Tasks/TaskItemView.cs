using Godot;
using System;

public partial class TaskItemView : ViewControl, IItemView
{
    public Label Label => GetNode<Label>("VBoxContainer/Label");
    public Control ProgressWorking => GetNode<Control>("VBoxContainer/DetailPanel/ProgressWorking");
    public Control ProductWorking => GetNode<Control>("VBoxContainer/DetailPanel/ProductWorking");
    public ProgressBar Progress => ProgressWorking.GetNode<ProgressBar>("ProgressBar");
    public Label ProductType => ProductWorking.GetNode<Label>("HBoxContainer/ProductType");
    public Label ProductValue => ProductWorking.GetNode<Label>("HBoxContainer/ProductValue");
    public Button Button => GetNode<Button>("Button");
    public object Id { get; set; }
}