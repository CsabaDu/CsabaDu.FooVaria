namespace CsabaDu.FooVaria.Proportions.Types;

public interface IProportionLimit : ISimpleRate, ILimiter<IProportionLimit, IBaseRate>, ICommonBase<IProportionLimit>
{
    IProportionLimit GetProportionLimit(IBaseRate baseRate, LimitMode limitMode);
    IProportionLimit GetProportionLimit(IBaseMeasure numerator, IBaseMeasure denominator, LimitMode limitMode);
    IProportionLimit GetProportionLimit(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement, LimitMode limitMode);
    IProportionLimit GetProportionLimit(Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit, LimitMode limitMode);
    IProportionLimit GetProportionLimit(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode, LimitMode limitMode);
    IProportionLimit GetProportionLimit(IBaseMeasure numerator, Enum denominatorMeasureUnit, LimitMode limitMode);
    IProportionLimit GetProportionLimit(IBaseMeasure numerator, MeasureUnitCode denominatorCode, LimitMode limitMode);
}
