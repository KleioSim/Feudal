namespace Feudal.Interfaces;

public interface IWorking
{
    string Id { get; }

    string Name { get; }

}

public interface IProgressWorking : IWorking
{
    void Finished(IWorkHood workHood);
    float GetStep(IWorkHood workHood);
}