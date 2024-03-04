namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Types;

public interface IBaseRate : IBaseQuantifiable, IQuantity<decimal>, IProportional<IBaseRate>, IExchangeable<IBaseRate>, IFit<IBaseRate>, IDenominate, IValidRateComponent, ILimitMode, IMeasureUnitCodes
{
    MeasureUnitCode GetNumeratorCode();
    MeasureUnitCode GetMeasureUnitCode(RateComponentCode rateComponentCode);
    IBaseRate GetBaseRate(IQuantifiable numerator, Enum denominator);
    IBaseRate GetBaseRate(IQuantifiable numerator, IMeasurable denominator);
    IBaseRate GetBaseRate(params IQuantifiable[] quantifiables);

    void ValidateRateComponentCode(RateComponentCode rateComponentCode);
}


    //IBaseRate GetBaseRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode);
    //IBaseRate GetBaseRate(IBaseRate baseRate);