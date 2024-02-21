namespace Feudal.Commands;

public class Command_SwitchCurrentWorking : ICommand
{
    public string WorkingId { get; init; }
}