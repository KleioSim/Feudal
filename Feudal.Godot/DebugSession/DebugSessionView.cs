using Godot;

public partial class DebugSessionView : ViewControl
{
    public override void _Process(double delta)
    {
        base._Process(delta);

        GetTree().ChangeSceneToFile("res://MainScene/MainScene.tscn");
    }
}
