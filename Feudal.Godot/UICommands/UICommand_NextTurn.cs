using Feudal.Interfaces;

namespace Feudal.Godot.UICommands;

internal class UICommand_NextTurn : UICommand
{
    public override string[] parameters { get; }

    public override Command type => Command.NextTurn;
}