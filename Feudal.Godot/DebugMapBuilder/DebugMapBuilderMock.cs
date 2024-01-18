using Feudal.Godot.Presents;

partial class DebugMapBuilderMock : MockControl<DebugMapBuilderView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            return new ModelMock() { Session = session };
        }
    }
}