using Feudal.Interfaces;

namespace Feudal.Godot.Presents;

partial class ResourceItemMock : MockControl<ResourceItemView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {

            var resource = new MockResource();
            View.Id = resource.Id;

            var session = new SessionMock();
            session.MockResources.Add(resource.Id, resource);

            return new ModelMock() { Session = session };
        }
    }
}

class MockResource : IResource
{
    public string Name => Id;

    public string Id { get; }

    private static int count;

    public MockResource()
    {
        Id = $"R{count++}";
    }
}