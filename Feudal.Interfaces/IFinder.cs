namespace Feudal.Interfaces;

public interface IFinder
{
    Func<(int x, int y), ITerrain> FindTerrain { get; }
    Func<(int x, int y), IWorkHood> FindWorkHoodByPos { get; }
    Func<string, IWorking> FindWorking { get; }
    Func<string, IWorkHood> FindWorkHood { get; }
    Func<(int x, int y), IEnumerable<IWorking>> FindWorkingsInTerrain { get; }
}