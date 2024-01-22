namespace Feudal.Interfaces;

public interface IWorking
{
    string Id { get; }

    string Name { get; }

}

public interface ICanFinishedWorking : IWorking
{
    void Finished(IWorkHood workHood);
}
