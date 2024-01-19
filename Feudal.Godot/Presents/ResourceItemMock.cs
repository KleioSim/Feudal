using Feudal.Interfaces;
using System.Collections.Generic;

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

            return new SessionModel() { Session = session };
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

    public IEnumerable<IWorking> GetWorkings()
    {
        throw new System.NotImplementedException();
    }
}