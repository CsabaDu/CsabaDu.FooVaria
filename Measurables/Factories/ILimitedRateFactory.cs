namespace CsabaDu.FooVaria.Measurables.Factories;

public interface ILimitedRateFactory : IRateFactory
{
    ILimitFactory LimitFactory { get; init; }

    ILimitedRate Create(ILimitedRate limitedRate, ILimit? limit);
    ILimitedRate Create(IMeasure numerator, string name, ValueType? quantity, ILimit? limit);
    ILimitedRate Create(IMeasure numerator, Enum measureUnit, ValueType? quantity, ILimit? limit);
    ILimitedRate Create(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity, ILimit? limit);
    ILimitedRate Create(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity, ILimit? limit);
    ILimitedRate Create(IMeasure numerator, IMeasurement measurement, ValueType? quantity, ILimit? limit);
    ILimitedRate Create(IMeasure numerator, IDenominator denominator, ILimit? limit);
    ILimitedRate Create(IRate rate, ILimit? limit);
}
