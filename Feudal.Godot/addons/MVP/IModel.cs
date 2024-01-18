using Feudal.Interfaces;

public interface IModel
{
    void OnCommand(UICommand command);
}

public abstract class UICommand
{
    public abstract object[] parameters { get; }

    public abstract Command type { get; }
}