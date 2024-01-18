using Feudal.Interfaces;

namespace Feudal.Godot.Presents;

internal class UIRefreshCommand : UICommand
{
    public override object[] parameters { get; }

    public override Command type { get; }
}
