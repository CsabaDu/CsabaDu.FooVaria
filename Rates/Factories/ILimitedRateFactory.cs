namespace CsabaDu.FooVaria.Rates.Factories;

public interface ILimitedRateFactory : IRateFactory, IFactory<ILimitedRate>
{
    ILimitFactory LimitFactory { get; init; }

    ILimitedRate Create(IMeasure numerator, string name, ValueType quantity, ILimit limit);
    ILimitedRate Create(IMeasure numerator, Enum denominatorMeasureUnit, ValueType quantity, ILimit limit);
    ILimitedRate Create(IMeasure numerator, string name, ILimit limit);
    ILimitedRate Create(IMeasure numerator, Enum denominatorMeasureUnit, ILimit limit);
    ILimitedRate Create(IMeasure numerator, IMeasurement measurement, ILimit limit);
    ILimitedRate Create(IMeasure numerator, IMeasurement measurement, ValueType quantity, ILimit limit);
    ILimitedRate Create(IMeasure numerator, IDenominator denominator, ILimit limit);
    ILimitedRate Create(IRate rate, ILimit? limit);
    ILimit CreateLimit(IDenominator denominator);
}
