namespace Feudal.Interfaces;

public interface ITask
{
    string Id { get; }

    string Desc { get; }

    ILabor Labor { get; }

    IWorking Working { get; }
}
