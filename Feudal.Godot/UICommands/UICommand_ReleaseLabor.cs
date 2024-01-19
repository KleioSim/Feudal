using Feudal.Interfaces;

namespace Feudal.Godot.UICommands;

class UICommand_ReleaseLabor : UICommand
{
    public override string[] parameters => new[] { ClanId, WorkHoodId };

    public override Command type => Command.ReleaseLabor;

    public string ClanId { get; init; }

    public string WorkHoodId { get; init; }
}
