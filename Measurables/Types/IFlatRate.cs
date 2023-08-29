namespace CsabaDu.FooVaria.Measurables.Types;

public interface IFlatRate : IRate, ICalculable, ICalculate<decimal, IFlatRate>, IMultiply<IMeasure, IMeasure>
{
    IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity = null);
    IFlatRate GetFlatRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity = null);
    IFlatRate GetFlatRate(IMeasure numerator, string customName, decimal? quantity = null);
    IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit, decimal? quantity = null);
    IFlatRate GetFlatRate(IMeasure numerator, IMeasurement measurement, decimal? quantity = null);
    IFlatRate GetFlatRate(IMeasure numerator, IDenominator? denominator = null);
    IFlatRate GetFlatRate(IRate rate);
    IFlatRate GetFlatRate(IFlatRate? other = null);
    IFlatRateFactory GetFlatRateFactory();
}
