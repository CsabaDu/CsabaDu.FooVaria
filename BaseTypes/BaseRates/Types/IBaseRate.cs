namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Types;

public interface IBaseRate : IQuantifiable, IQuantity<decimal>, IProportional<IBaseRate>, IFit<IBaseRate>, IExchangeable<IBaseRate>, IDenominate, ILimitable
{
    MeasureUnitCode? this[RateComponentCode rateComponentCode] { get; }

    MeasureUnitCode GetNumeratorMeasureUnitCode();

    IBaseRate GetBaseRate(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode);
    IBaseRate GetBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode);
    IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
    IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement);
    IBaseRate GetBaseRate(params IBaseMeasure[] baseMeasures);
}
