using Feudal.Commands;
using Feudal.Interfaces;

public interface IModel
{
    void OnCommand(ICommand command);
}

public abstract class UICommand
{
    public abstract string[] parameters { get; }

    public abstract Command type { get; }
}