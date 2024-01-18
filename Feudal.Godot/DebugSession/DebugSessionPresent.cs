using Feudal.Godot.Presents;
using Feudal.SessionBuilders;
using System;

public partial class DebugSessionPresent : PresentControl<DebugSessionView, ISessionModel>
{
    public override void _Ready()
    {
        base._Ready();

        Model = new SessionModel() { Session = SessionBuilder.BuildDebug() };
    }

    protected override void Initialize(DebugSessionView view, ISessionModel model)
    {

    }

    protected override void Update(DebugSessionView view, ISessionModel model)
    {

    }
}
