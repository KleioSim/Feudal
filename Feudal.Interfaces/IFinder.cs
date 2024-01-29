namespace Feudal.Interfaces;

public interface IFinder
{
    Func<(int x, int y), ITerrain> FindTerrain { get; }
    Func<(int x, int y), string> FindWorkHoodId { get; }
    Func<string, IWorking> FindWorking { get; }
}