namespace Feudal.Commands;

public class Command_DiscoverTerrain : ICommand
{
    public string Position_X { get; init; }
    public string Position_Y { get; init; }
}