namespace Feudal.Godot.Presents;

internal class ModelMock : ISessionModel
{
    public ISession Session { get; set; }

    public void OnCommand(UICommand command)
    {

    }
}
