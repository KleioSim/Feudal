namespace Feudal.Interfaces;

using System.Collections.Generic;

public interface ITerrain
{
    (int x, int y) Position { get; }
    IReadOnlySet<IResource> Resources { get; }
    TerrainType TerrainType { get; }
    bool IsDiscoverd { get; }
    IWorkHood WorkHood { get; }
}
