﻿using Feudal.Interfaces;

namespace Feudal.Resources;

class Deer : IResource
{
    public string Id => Name;

    public string Name => this.GetType().Name;

    public IEnumerable<string> GetWorkings()
    {
        return new[] { "Hunting" };
    }
}