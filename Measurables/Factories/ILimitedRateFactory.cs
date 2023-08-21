namespace CsabaDu.FooVaria.Measurables.Factories;

public interface ILimitedRateFactory : IRateFactory
{
    ILimitFactory LimitFactory { get; init; }

    ILimitedRate Create(ILimitedRate other);
    ILimitedRate Create(IMeasure numerator, Enum measureUnit, decimal? exchangeRate, ValueType? quantity, ILimit? limit);
    ILimitedRate Create(IMeasure numerator, IMeasurement measurement, ValueType? quantity, ILimit? limit);
    ILimitedRate Create(IMeasure numerator, IDenominator denominator, ILimit? limit);
    ILimitedRate Create(IRate rate, ILimit? limit);
    ILimitedRate Create<T>(IRate rate, IRateComponent? rateComponent);
}
