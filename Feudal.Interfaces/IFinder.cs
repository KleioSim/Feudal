namespace Feudal.Interfaces;

public interface IFinder
{
    Func<(int x, int y), ITerrain> FindTerrain { get; }
    Func<string, IWorking> FindWorking { get; }
    Func<string, IClan> FindClan { get; }
    Func<(int x, int y), IEnumerable<IWorking>> FindWorkingsInTerrain { get; }
    Func<TerrainType, IEnumerable<IResource>> FindResourceByTerrainType { get; }
    Func<string, ILabor> FindLabor { get; }
    Func<ILabor, ITask> FindTaskByLabor { get; }
    Func<ITerrain, IEnumerable<ItemChange<IWorking>>> FindWorkingChanges { get; }
    Func<ITerrain, IEnumerable<IWorking>> FindTerrainWorkings { get; }
}

public class ItemChange<T>
{
    public ItemChangeType ChangeType { get; init; }
    public T Item { get; init; }
}

public enum ItemChangeType
{
    Remove,
    Add
}