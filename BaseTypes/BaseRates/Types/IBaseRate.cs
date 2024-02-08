namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Types;

public interface IBaseRate : IQuantifiable, IQuantity<decimal>, IProportional<IBaseRate>, IExchangeable<IBaseRate>, IFit<IBaseRate>, IDenominate, ILimitable
{
    object? this[RateComponentCode rateComponentCode] { get; }

    MeasureUnitCode GetNumeratorMeasureUnitCode();
    MeasureUnitCode GetMeasureUnitCode(RateComponentCode rateComponentCode);

    IBaseRate GetBaseRate(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode);
    IBaseRate GetBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode);
    IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
    IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement);
    IBaseRate GetBaseRate(params IBaseMeasure[] baseMeasures);
}
