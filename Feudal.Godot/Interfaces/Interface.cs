﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

public interface ISession
{
    IClan PlayerClan { get; }
    IReadOnlyDictionary<object, ITask> Tasks { get; }
    IReadOnlyDictionary<object, IResource> Resources { get; }
    IReadOnlyDictionary<(int x, int y), ITerrain> Terrains { get; }
    IReadOnlyDictionary<object, IWorkHood> WorkHoods { get; }
    IReadOnlyDictionary<object, IWorking> Workings { get; }
}

public interface ITask
{
    string Id { get; }

    string Desc { get; }

    float Percent { get; }

    //string WorkHoodId { get; }
}

public interface IClan
{
    string Id { get; }

    IReadOnlyDictionary<ProductType, IProduct> Products { get; }
}

public enum ProductType
{
    Food,
    Bronze,
}

public interface IProduct
{
    ProductType Type { get; }
    float Current { get; }
    float Surplus { get; }
}

public interface IResource
{
    string Id { get; }

    string Name { get; }
}

public interface ITerrain
{
    (int x, int y) Position { get; }
    IReadOnlySet<IResource> Resources { get; }
}

public interface IWorkHood
{
    string Id { get; }

    IWorking CurrentWorking { get; }

    IEnumerable<IWorking> OptionWorkings { get; }
}

public interface IWorking
{
    string Id { get; }

    string Name { get; }
}