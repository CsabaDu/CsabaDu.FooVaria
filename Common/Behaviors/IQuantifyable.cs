using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IQuantifyable<T> where T : struct
    {
        T GetQuantity();
        decimal GetDefaultQuantity();

        void ValidateQuantity(T quantity);
    }
}
