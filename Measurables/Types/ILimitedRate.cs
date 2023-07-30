namespace CsabaDu.FooVaria.Measurables.Types;

public interface ILimitedRate : IRate, ILimiter<ILimitedRate, IMeasure>
{
    ILimit Limit { get; init; }

    ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal? exchangeRate = null, ValueType? quantity = null, ILimit? limit = null);
    ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, ValueType? quantity = null, ILimit? limit = null);
    ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null);
    ILimitedRate GetLimitedRate(IRate rate, ILimit? limit = null);
    ILimitedRate GetLimitedRate(ILimitedRate? other = null);
}
