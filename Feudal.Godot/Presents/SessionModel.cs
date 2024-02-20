using Feudal.Commands;
using Feudal.Interfaces;

namespace Feudal.Godot.Presents;

internal class SessionModel : ISessionModel
{
    public ISession Session { get; init; }

    public void OnCommand(ICommand command)
    {
        Session.OnCommand(command);
    }
}
