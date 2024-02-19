namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Types
{
    public interface IBaseRate : IBaseQuantifiable, IQuantity<decimal>, IProportional<IBaseRate>, IExchangeable<IBaseRate>, IFit<IBaseRate>, IDenominate, ILimitable<IBaseRate>, ILimitMode
    {
        //object? this[RateComponentCode rateComponentCode] { get; }

        MeasureUnitCode GetNumeratorCode();
        MeasureUnitCode GetMeasureUnitCode(RateComponentCode rateComponentCode);

        IBaseRate GetBaseRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode);
        IBaseRate GetBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorCode);
        IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
        IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement);
        IBaseRate GetBaseRate(params IBaseMeasure[] baseMeasures);
        IBaseRate GetBaseRate(IBaseRate baseRate);
    }
}
