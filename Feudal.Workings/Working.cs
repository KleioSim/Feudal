using Feudal.Interfaces;

namespace Feudal.Workings;

internal class Working : IWorking
{
    public static IFinder Finder { get; set; }

    public string Id { get; }

    public string Name => Id;

    public IWorkHood WorkHood { get; set; }

    protected ISession session;

    public Working(ISession session)
    {
        Id = this.GetType().Name;

        this.session = session;
    }
}
