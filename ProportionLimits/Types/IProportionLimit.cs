﻿namespace CsabaDu.FooVaria.ProportionLimits.Types;

public interface IProportionLimit : ISimpleRate, ILimiter<IProportionLimit, IBaseRate>
{
    LimitMode LimitMode { get; init; }

    IProportionLimit GetProportionLimit(IBaseRate baseRate, LimitMode limitMode);
    IProportionLimit GetProportionLimit(IQuantifiable numerator, IQuantifiable denominator, LimitMode limitMode);
    IProportionLimit GetProportionLimit(IQuantifiable numerator, IBaseMeasurement denominator, LimitMode limitMode);
    IProportionLimit GetProportionLimit(IQuantifiable numerator, Enum denominator, LimitMode limitMode);
    IProportionLimit GetProportionLimit(Enum numeratorContext, decimal quantity, Enum denominator, LimitMode limitMode);
}

    //IProportionLimit GetProportionLimit(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode, LimitMode limitMode);
    //IProportionLimit GetProportionLimit(IQuantifiable numerator, MeasureUnitCode denominatorCode, LimitMode limitMode);