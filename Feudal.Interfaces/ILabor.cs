namespace Feudal.Interfaces;

public interface ILabor
{
    string Id { get; }

    IClan From { get; }

    ITask Task { get; }
}
