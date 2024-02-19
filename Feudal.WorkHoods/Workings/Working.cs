using Feudal.Interfaces;

namespace Feudal.WorkHoods.Workings;

internal class Working : IWorking
{
    public static IFinder Finder { get; set; }

    private static int count;

    public string Id { get; }

    public string Name => Id;

    public IWorkHood WorkHood { get; }

    public Working(IWorkHood workHood)
    {
        Id = $"{this.GetType().Name}_{count++}";
        WorkHood = workHood;
    }
}
