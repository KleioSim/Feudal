using Feudal.Godot.Presents;

partial class MapBuilderDebugMock : MockControl<MapBuilderDebugView, ISessionModel>
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