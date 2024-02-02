namespace CsabaDu.FooVaria.Proportions.Factories;

public interface IProportionLimitFactory : IBaseRateFactory, IFactory<IProportionLimit>
{
    IProportionLimit Create(IBaseRate baseRate, LimitMode limitMode);
    IProportionLimit Create(IBaseMeasure numerator, IBaseMeasure denominator, LimitMode limitMode);
    IProportionLimit Create(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement, LimitMode limitMode);
    IProportionLimit Create(Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit, LimitMode limitMode);
    IProportionLimit Create(MeasureUnitCode numeratorMeasureUnitCode, decimal ndefaultQuantity, MeasureUnitCode denominatorMeasureUnitCode, LimitMode limitMode);
    IProportionLimit Create(IBaseMeasure numerator, Enum denominatorMeasureUnit, LimitMode limitMode);
    IProportionLimit Create(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode, LimitMode limitMode);
}
