namespace Feudal.Commands;

public class Command_ReleaseLabor : ICommand
{
    public string ClanId { get; init; }
    public string WorkingId { get; init; }
}