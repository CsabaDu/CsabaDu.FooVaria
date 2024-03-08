namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Types;

public interface IBaseRate : IBaseQuantifiable, IQuantity<decimal>, IProportional<IBaseRate>, IExchangeable<IBaseRate>, IFit<IBaseRate>, IDenominate, IValidRateComponent, ILimitMode, IMeasureUnitCodes
{
    MeasureUnitCode GetNumeratorCode();
    MeasureUnitCode GetMeasureUnitCode(RateComponentCode rateComponentCode);
    IBaseRate GetBaseRate(IQuantifiable numerator, Enum denominator);
    IBaseRate GetBaseRate(IQuantifiable numerator, IMeasurable denominator);
    IBaseRate GetBaseRate(IQuantifiable numerator, IQuantifiable denominator);

    void ValidateRateComponentCode(RateComponentCode rateComponentCode, string paramName);
}
