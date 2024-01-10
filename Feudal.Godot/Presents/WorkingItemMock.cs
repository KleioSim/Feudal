namespace Feudal.Godot.Presents;

partial class WorkingItemMock : MockControl<WorkingItemView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {

            var working = new MockWorking();
            View.Id = working.Id;

            var session = new SessionMock();
            session.MockWorkings.Add(working.Id, working);

            return new ModelMock() { Session = session };
        }
    }
}

class MockWorking : IWorking
{
    public string Id { get; set; }

    public string Name => Id;

    private static int Count = 0;

    public MockWorking()
    {
        Id = $"WG{Count++}";
    }
}