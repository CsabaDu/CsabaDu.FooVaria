using CsabaDu.FooVaria.Common.Types;

namespace CsabaDu.FooVaria.Measures.Behaviors;

public interface ILimitable : IFit<IBaseMeasure>
{
    bool? FitsIn(ILimiter? limiter);
}
