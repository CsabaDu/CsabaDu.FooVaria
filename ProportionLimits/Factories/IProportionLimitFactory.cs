namespace CsabaDu.FooVaria.ProportionLimits.Factories;

public interface IProportionLimitFactory : ISimpleRateFactory, IFactory<IProportionLimit>
{
    IProportionLimit Create(IBaseRate baseRate, LimitMode limitMode);
    IProportionLimit Create(IBaseMeasure numerator, IBaseMeasure denominator, LimitMode limitMode);
    IProportionLimit Create(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement, LimitMode limitMode);
    IProportionLimit Create(Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit, LimitMode limitMode);
    IProportionLimit Create(MeasureUnitCode numeratorCode, decimal ndefaultQuantity, MeasureUnitCode denominatorCode, LimitMode limitMode);
    IProportionLimit Create(IBaseMeasure numerator, Enum denominatorMeasureUnit, LimitMode limitMode);
    IProportionLimit Create(IBaseMeasure numerator, MeasureUnitCode denominatorCode, LimitMode limitMode);
}
