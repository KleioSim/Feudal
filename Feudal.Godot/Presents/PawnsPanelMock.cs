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
                    WorkHoodId = workHoods[0].Id,
                    ClanId = clans[0].Id,
                },
                new TaskMock()
                {
                    WorkHoodId = workHoods[1].Id,
                    ClanId = clans[1].Id,
                },
                new TaskMock()
                {
                    WorkHoodId = workHoods[2].Id,
                    ClanId = clans[2].Id,
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