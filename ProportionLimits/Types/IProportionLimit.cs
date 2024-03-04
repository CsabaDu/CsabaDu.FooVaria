namespace CsabaDu.FooVaria.ProportionLimits.Types;

public interface IProportionLimit : ISimpleRate, ILimiter<IProportionLimit, IBaseRate>, ICommonBase<IProportionLimit>
{
    LimitMode LimitMode { get; init; }

    IProportionLimit GetProportionLimit(IBaseRate baseRate, LimitMode limitMode);
    IProportionLimit GetProportionLimit(IQuantifiable numerator, IQuantifiable denominator, LimitMode limitMode);
    IProportionLimit GetProportionLimit(IQuantifiable numerator, IBaseMeasurement denominatorMeasurement, LimitMode limitMode);
    IProportionLimit GetProportionLimit(IQuantifiable numerator, Enum denominatorContext, LimitMode limitMode);
    IProportionLimit GetProportionLimit(Enum numeratorContext, ValueType quantity, Enum denominatorContext, LimitMode limitMode);
}

    //IProportionLimit GetProportionLimit(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode, LimitMode limitMode);
    //IProportionLimit GetProportionLimit(IQuantifiable numerator, MeasureUnitCode denominatorCode, LimitMode limitMode);