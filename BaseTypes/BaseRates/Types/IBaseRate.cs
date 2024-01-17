namespace CsabaDu.FooVaria.BaseRates.Types;

public interface IBaseRate : IQuantifiable, IQuantity<decimal>, IProportional<IBaseRate>, IDenominate
{
    MeasureUnitCode? this[RateComponentCode rateComponentCode] { get; }

    MeasureUnitCode GetNumeratorMeasureUnitCode();

    IBaseRate GetBaseRate(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode);
    IBaseRate GetBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode);
    IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
    IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement);
    IBaseRate GetBaseRate(params IBaseMeasure[] baseMeasures);
}
