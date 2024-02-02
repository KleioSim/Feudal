using Feudal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feudal.Workings;

internal class Hunting : Working, IProductWorking
{
    public Hunting(ISession session) : base(session)
    {
    }

    public ProductType ProductType => throw new NotImplementedException();

    public IEffectValue GetEffectValue(string workHoodId)
    {
        throw new NotImplementedException();
    }
}
