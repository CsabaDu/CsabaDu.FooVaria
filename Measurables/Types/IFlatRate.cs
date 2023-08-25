namespace CsabaDu.FooVaria.Measurables.Types;

public interface IFlatRate : IRate, ICalculable, ICalculate<decimal, IFlatRate>, IMultiply<IMeasure, IMeasure>
{
    IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit, decimal? exchangeRate = null, ValueType? quantity = null);
    IFlatRate GetFlatRate(IMeasure numerator, IMeasurement measurement, ValueType? quantity = null);
    IFlatRate GetFlatRate(IMeasure numerator, IDenominator? denominator = null);
    IFlatRate GetFlatRate(IRate rate);
    IFlatRate GetFlatRate(IFlatRate? other = null);
}
