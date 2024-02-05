using Feudal.Interfaces;
using Godot;
using System.Collections.Generic;

namespace Feudal.Godot.Presents;

partial class PawnsPanelMock : MockControl<PawnsPanelView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            var workHoods = new List<MockWorkHood>()
            {
                new MockTerrainWorkHood()
                {
                    Position = (0,0)
                },
                new MockTerrainWorkHood()
                {
                    Position = (1,1)
                },
                new MockTerrainWorkHood()
                {
                    Position = (1,0)
                }
            };

            foreach (var workHood in workHoods)
            {
                session.MockWorkHoods.Add(workHood.Id, workHood);
            }

            var clans = new List<ClanMock>()
            {
                new ClanMock(),
                new ClanMock(),
                new ClanMock()
            };

            foreach (var clan in clans)
            {
                session.MockClans.Add(clan.Id, clan);
            }

            var tasks = new List<TaskMock>()
            {
                new TaskMock()
                {
                    WorkHood = workHoods[0],
                    Clan = clans[0],
                },
                new TaskMock()
                {
                    WorkHood = workHoods[1],
                    Clan = clans[1],
                },
                new TaskMock()
                {
                    WorkHood = workHoods[2],
                    Clan = clans[2],
                }
            };

            foreach (var task in tasks)
            {
                session.MockTasks.Add(task.Id, task);
            }

            return new SessionModel() { Session = session };
        }
    }
}