using Godot;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class TilemapMock : MockControl<TilemapView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            var terrain = new MockTerrain();
            session.MockTerrains.Add(terrain.Position, terrain);

            for (int i = 0; i < 3; i++)
            {
                var resource = new MockResource();
                terrain.MockResources.Add(resource);
                session.MockResources.Add(resource.Id, resource);
            }

            var workHood = new MockWorkHood();
            terrain.WorkHoodId = workHood.Id;

            session.MockWorkHoods.Add(workHood.Id, workHood);

            for (int i = 0; i < 2; i++)
            {
                var working = new MockWorking();

                workHood.MockOptionWorkings.Add(working);
                session.MockWorkings.Add(working.Id, working);
            }

            workHood.CurrentWorking = workHood.OptionWorkings.First();

            var clan = new ClanMock();
            session.MockClans.Add(clan.Id, clan);

            var task = new TaskMock();
            task.ClanId = clan.Id;
            task.WorkHoodId = workHood.Id;

            session.MockTasks.Add(task.Id, task);

            return new ModelMock() { Session = session };
        }
    }
}