using Feudal.Interfaces;

namespace Feudal.Godot.Presents;

internal class UIRefreshCommand : UICommand
{
    public override object[] parameters { get; }

    public override Command type { get; }
}


internal class OccupyLaborCommand : UICommand
{
    public override object[] parameters => new[] { ClanId, WorkHoodId };

    public override Command type => Command.OccupyLabor;

    public string ClanId { get; init; }

    public string WorkHoodId { get; init; }
}