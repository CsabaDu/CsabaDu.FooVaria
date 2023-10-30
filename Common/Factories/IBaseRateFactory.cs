using CsabaDu.FooVaria.Common.Types;

namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseRateFactory : IBaseMeasurableFactory
{
    IBaseRate Create(IQuantifiable numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
}