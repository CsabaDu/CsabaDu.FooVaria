namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Factories;

public interface IBaseRateFactory : IBaseQuantifiableFactory
{
    IBaseRate CreateBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement);
    IBaseRate CreateBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
    IBaseRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorCode);
    IBaseRate CreateBaseRate(params IBaseMeasure[] baseMeasures);
    IBaseRate CreateBaseRate(IBaseRate baseRate);
}