using Feudal.Interfaces;

namespace Feudal.Godot.UICommands;

class UICommand_OccupyLabor : UICommand
{
    public override string[] parameters => new[] { ClanId, WorkingId };

    public override Command type => Command.OccupyLabor;

    public string ClanId { get; init; }

    public string WorkingId { get; init; }
}
