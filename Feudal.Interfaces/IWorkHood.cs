﻿namespace Feudal.Interfaces;

using System.Collections.Generic;

public interface IWorkHood
{
    string Id { get; }

    IWorking CurrentWorking { get; }

    IEnumerable<IWorking> OptionWorkings { get; }
}


public interface ITerrainWorkHood : IWorkHood
{
    (int x, int y) Position { get; }
}