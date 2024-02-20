using Godot;
using System;

public partial class ProgressWorkingPawnView : WorkingPawnView
{
    public Node RadialProgress => GetNode("RadialProgress");
    public Label Label => GetNode<Label>("Panel/VBoxContainer/Label");
    public Label Value => GetNode<Label>("Panel/VBoxContainer/Value");

    public float ProgressValue
    {
        get
        {
            return RadialProgress.Get("progress").As<float>();
        }
        set
        {
            RadialProgress.Set("progress", value);
        }
    }
}