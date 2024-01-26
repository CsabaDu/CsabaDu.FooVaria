namespace CsabaDu.FooVaria.Proportions.Types
{
    public interface IProportion : IBaseRate
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
        //IProportion<TDEnum> GetProportion(MeasureUnitCode numeratorMeasureUnitCode, decimal numeratorDefaultQuantity, TDEnum denominatorMeasureUnit);
    }

    public interface IProportion<TNEnum, TDEnum> : IProportion<TDEnum>, IMeasureUnit<TNEnum>
        where TNEnum : struct, Enum
        where TDEnum : struct, Enum
    {
        IProportion<TNEnum, TDEnum> GetProportion(TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit);
        decimal GetQuantity(TNEnum numeratorMeasureUnit, TDEnum denominatorMeasureUnit);
        decimal GetQuantity(TNEnum numeratorMeasureUnit);
    }

    public interface IProportionLimit : IProportion, ILimiter<IProportionLimit>, ICommonBase<IProportionLimit>
    {
        IProportionLimit GetProportionLimit(IBaseRate baseRate, LimitMode limitMode);
        IProportionLimit GetProportionLimit(IBaseMeasure numerator, IBaseMeasure denominator, LimitMode limitMode);
        IProportionLimit GetProportionLimit(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement, LimitMode limitMode);
        IProportionLimit GetProportionLimit(Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit, LimitMode limitMode);
        IProportionLimit GetProportionLimit(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode, LimitMode limitMode);
        IProportionLimit GetProportionLimit(IBaseMeasure numerator, Enum denominatorMeasureUnit, LimitMode limitMode);
        IProportionLimit GetProportionLimit(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode, LimitMode limitMode);

    }
}
