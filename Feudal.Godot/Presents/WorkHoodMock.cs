using Godot;
using System.Linq;

namespace Feudal.Godot.Presents;

[Tool]
partial class WorkHoodMock : MockControl<WorkHoodView, ISessionModel>
{
    [Export]
    public bool IsOccupyClan
    {
        get
        {
            return Present.Model.Session.Tasks.Values.Any(x => x.WorkHoodId == View.Id);
        }
        set
        {
            var session = Present.Model.Session as SessionMock;

            var task = session.MockTasks.Values.SingleOrDefault(x => x.WorkHoodId == View.Id) as TaskMock;
            if (value)
            {
                if(task != null)
                {
                    return;
                }

                task = new TaskMock();
                task.ClanId = session.Clans.Values.Last().Id;
                task.WorkHoodId = View.Id;

                session.MockTasks.Add(task.Id, task);
            }
            else
            {
                if (task == null)
                {
                    return;
                }

                session.MockTasks.Remove(task.Id);
            }

            Present.SendCommand(new UIRefreshCommand());
        }
    }


    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            var workHood = new MockWorkHood();
            View.Id = workHood.Id;

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