namespace CsabaDu.FooVaria.Measurables.Factories;

public interface ILimitedRateFactory : IRateFactory
{
    ILimitFactory LimitFactory { get; init; }

    IFlatRate Create(IFlatRate limitedRate, ILimit? limit);
    IFlatRate Create(IMeasure numerator, string name, ValueType? quantity, ILimit? limit);
    IFlatRate Create(IMeasure numerator, Enum measureUnit, ValueType? quantity, ILimit? limit);
    IFlatRate Create(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity, ILimit? limit);
    IFlatRate Create(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity, ILimit? limit);
    IFlatRate Create(IMeasure numerator, IMeasurement measurement, ValueType? quantity, ILimit? limit);
    IFlatRate Create(IMeasure numerator, IDenominator denominator, ILimit? limit);
    IFlatRate Create(IRate rate, ILimit? limit);
}
