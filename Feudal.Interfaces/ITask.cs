namespace Feudal.Interfaces;

public interface ITask
{
    string Id { get; }

    string Desc { get; }

    float Percent { get; }
    float Step { get; }

    IClan Clan { get; }
    IWorkHood WorkHood { get; }
}
