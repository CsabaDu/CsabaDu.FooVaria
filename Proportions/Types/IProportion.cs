namespace CsabaDu.FooVaria.Proportions.Types
{
    public interface IProportion : IBaseRate, ILimitable<IProportion>
    {
        MeasureUnitCode NumeratorMeasureUnitCode { get; init; }
        decimal DefaultQuantity { get; init; }

        IProportion GetProportion(IBaseRate baseRate);
    }

    public interface IProportion<TDEnum> : IProportion, IDenominate<IMeasure, TDEnum>
        where TDEnum : struct, Enum
    {
        IProportion GetProportion(IBaseMeasure numerator, IBaseMeasure denominator);
        IProportion<TDEnum> GetProportion(IBaseMeasure numerator, TDEnum denominatorMeasureUnit);
        decimal GetQuantity(TDEnum denominatorMeasureUnit);
    }

    public interface IProportion<TNEnum, TDEnum> : IProportion<TDEnum>, IMeasureUnit<TNEnum>
        where TNEnum : struct, Enum
        where TDEnum : struct, Enum
    {
        IProportion<TNEnum, TDEnum> GetProportion(TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit);
        decimal GetQuantity(TNEnum numeratorMeasureUnit, TDEnum denominatorMeasureUnit);
        decimal GetQuantity(TNEnum numeratorMeasureUnit);
    }
}
