﻿using Feudal.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class WorkingArrayMock : MockControl<WorkingArrayView, ISessionModel>
{
    public override ISessionModel Mock
    {
        get
        {
            var session = new SessionMock();

            var workHood = session.GenerateWorkHood();
            View.WorkHoodId = workHood.Id;

            for (int i = 0; i < 2; i++)
            {
                session.GenerateProgressWorking(workHood);
            }

            for (int i = 0; i < 2; i++)
            {
                session.GenerateProductWorking(workHood);
            }

            var task = session.GenerateTask();
            task.ClanId = session.GenerateClan().Id;
            task.WorkHoodId = workHood.Id;

            return new SessionModel() { Session = session };
        }
    }
}

class MockWorkHood : IWorkHood
{
    private static int count = 0;

    public string Id { get; }

    public IWorking CurrentWorking { get; set; }

    public IEnumerable<IWorking> OptionWorkings => MockOptionWorkings;

    public List<IWorking> MockOptionWorkings = new List<IWorking>();

    public MockWorkHood()
    {
        Id = $"WORK_HOOD{count++}";
    }
}

class MockTerrainWorkHood : MockWorkHood, ITerrainWorkHood
{
    public (int x, int y) Position { get; set; }
}