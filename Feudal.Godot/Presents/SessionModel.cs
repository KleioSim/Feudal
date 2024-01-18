using Feudal.Interfaces;

namespace Feudal.Godot.Presents;

internal class SessionModel : ISessionModel
{
    public ISession Session { get; init; }

    public void OnCommand(UICommand command)
    {
        Session.OnCommand(command.type, command.parameters);
    }
}
