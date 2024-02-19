namespace Feudal.Interfaces;

public interface ITask
{
    string Id { get; }

    string Desc { get; }

    IClan Clan { get; }
    //IWorkHood WorkHood { get; }
    IWorking Working { get; }
}
