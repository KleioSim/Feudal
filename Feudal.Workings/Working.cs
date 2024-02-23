using Feudal.Interfaces;

namespace Feudal.Workings;

internal abstract class Working : IWorking
{
    public static IFinder Finder { get; set; }

    private static int count;

    public string Id { get; }

    public string Name => Id;

    public IWorkHood WorkHood { get; }

    public abstract IEffectValue GetEffectValue();

    public Working(IWorkHood workHood)
    {
        Id = $"{this.GetType().Name}_{count++}";
        WorkHood = workHood;
    }
}
