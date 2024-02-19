using Feudal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feudal.WorkHoods.Workings;

internal class Hunting : Working, IProductWorking
{
    public Hunting(IWorkHood workHood) : base(workHood)
    {
    }

    public ProductType ProductType => throw new NotImplementedException();

    public IEffectValue GetEffectValue(string workHoodId)
    {
        throw new NotImplementedException();
    }
}
