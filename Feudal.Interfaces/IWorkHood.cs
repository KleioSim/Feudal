namespace Feudal.Interfaces;

using System.Collections.Generic;

public interface IWorkHood
{
    string Id { get; }

    IWorking CurrentWorking { get; }

    IEnumerable<IWorking> OptionWorkings { get; }
}
