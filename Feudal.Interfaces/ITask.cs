namespace Feudal.Interfaces;

public interface ITask
{
    string Id { get; }

    string Desc { get; }

    float Percent { get; }
    string WorkHoodId { get; }
    string ClanId { get; }
}
