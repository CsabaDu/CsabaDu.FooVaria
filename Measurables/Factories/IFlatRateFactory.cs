namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IFlatRateFactory : IRateFactory
{
    IFlatRate Create(IFlatRate other);
    IFlatRate Create(IMeasure numerator, Enum measureUnit, decimal? exchangeRate, ValueType? quantity);
    IFlatRate Create(IMeasure numerator, IMeasurement measurement, ValueType? quantity);
    IFlatRate Create(IMeasure numerator, IDenominator denominator);
    IFlatRate? Create(IRate rate, IRateComponent? rateComponent);
}
