namespace CsabaDu.FooVaria.Proportions.Types
{
    public interface IProportion : IBaseRate
    {
        MeasureUnitTypeCode NumeratorMeasureUnitTypeCode { get; init; }
        MeasureUnitTypeCode? this[RateComponentCode rateComponentCode] { get; }

        IProportion GetProportion(IBaseRate baseRate);
        IProportion GetProportion(IRateComponent numerator, IRateComponent denominator);
    }

    public interface IProportion<TDEnum> : IProportion, IDenominate<IBaseMeasure, TDEnum>
        where TDEnum : struct, Enum
    {
        IProportion<TDEnum> GetProportion(IBaseMeasure numerator, TDEnum denominatorMeasureUnit);
        IProportion<TDEnum> GetProportion(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal numeratorDefaultQuantity, TDEnum denominatorMeasureUnit);
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
