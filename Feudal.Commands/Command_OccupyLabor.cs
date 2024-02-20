namespace Feudal.Commands;

public class Command_OccupyLabor : ICommand
{
    public string ClanId { get; init; }
    public string WorkingId { get; init; }
}
