namespace CsabaDu.FooVaria.Rates.Factories;

public interface ILimitedRateFactory : IRateFactory, IFactory<ILimitedRate>
{
    ILimitFactory LimitFactory { get; init; }

    ILimitedRate Create(IMeasure numerator, string name, ValueType denominatorQuantity, ILimit limit);
    ILimitedRate Create(IMeasure numerator, string name, ILimit limit);
    ILimitedRate Create(IMeasure numerator, Enum denominatorMeasureUnit, ValueType denominatorQuantity, ILimit limit);
    ILimitedRate Create(IMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode, ILimit limit);
    ILimitedRate Create(IMeasure numerator, IMeasurement denominatorMeasurement, ILimit limit);
    ILimitedRate Create(IMeasure numerator, IDenominator denominator, ILimit limit);
    ILimitedRate Create(IRate rate, ILimit limit);
}
