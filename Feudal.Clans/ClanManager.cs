namespace Feudal.Clans;

public partial class ClanManager
{

    internal void Initialize()
    {
        for (int i = 0; i < 3; i++)
        {
            var clan = new Clan($"clan{i}", i * 1000);
            dict.Add(clan.Id, clan);
        }
    }
}