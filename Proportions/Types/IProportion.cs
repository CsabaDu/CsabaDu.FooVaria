﻿namespace CsabaDu.FooVaria.Proportions.Types
{
    public interface IProportion : IBaseRate
    {
        MeasureUnitCode NumeratorMeasureUnitCode { get; init; }
        decimal DefaultQuantity { get; init; }
        //MeasureUnitCode? this[RateComponentCode rateComponentCode] { get; }

        IProportion GetProportion(IBaseRate baseRate);
        IProportion GetProportion(IBaseMeasure numerator, IBaseMeasure denominator);
    }

    public interface IProportion<TDEnum> : IProportion, IDenominate<IBaseMeasure, TDEnum>
        where TDEnum : struct, Enum
    {
        IProportion<TDEnum> GetProportion(IBaseMeasure numerator, TDEnum denominatorMeasureUnit);
        IProportion<TDEnum> GetProportion(MeasureUnitCode numeratorMeasureUnitCode, decimal numeratorDefaultQuantity, TDEnum denominatorMeasureUnit);
        decimal GetQuantity(TDEnum denominatorMeasureUnit);
    }

    public interface IProportion<TNEnum, TDEnum> : IProportion<TDEnum>, IMeasureUnit<TNEnum>
        where TNEnum : struct, Enum
        where TDEnum : struct, Enum
    {
        IProportion<TNEnum, TDEnum> GetProportion(TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit);
        decimal GetQuantity(TNEnum numeratorMeasureUnit, TDEnum denominatorMeasureUnit);
    }
}
