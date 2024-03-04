using CsabaDu.FooVaria.BaseTypes.Common.Factories;
using CsabaDu.FooVaria.BaseTypes.Measurables.Types;

namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Factories;

public interface IBaseRateFactory : IBaseQuantifiableFactory/*, IFactory<IBaseRate>*/
{
    IBaseRate CreateBaseRate(IQuantifiable numerator, IMeasurable denominator);
    IBaseRate CreateBaseRate(IQuantifiable numerator, Enum denominatorContext);
    //IBaseRate CreateBaseRate(IQuantifiable numerator, MeasureUnitCode denominatorCode);
    IBaseRate CreateBaseRate(params IQuantifiable[] quantifiables);
    //IBaseRate CreateBaseRate(IBaseRate baseRate);
}