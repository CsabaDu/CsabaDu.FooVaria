using CsabaDu.FooVaria.BaseTypes.Common.Factories;

namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Factories;

public interface IBaseRateFactory : IQuantifiableFactory
{
    IBaseRate CreateBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement);
    IBaseRate CreateBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
    IBaseRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode);
    IBaseRate CreateBaseRate(params IBaseMeasure[] baseMeasures);
    IBaseRate CreateBaseRate(IBaseRate baseRate);
}