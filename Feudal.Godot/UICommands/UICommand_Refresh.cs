using Feudal.Interfaces;

namespace Feudal.Godot.UICommands;

class UICommand_Refresh : UICommand
{
    public override string[] parameters { get; }

    public override Command type { get; }
}