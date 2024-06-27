namespace CsabaDu.FooVaria.Rates.Factories;

public interface ILimitedRateFactory : IRateFactory, IDeepCopyFactory<ILimitedRate>
{
    ILimitFactory LimitFactory { get; init; }

    ILimitedRate Create(IMeasure numerator, string name, ValueType denominatorQuantity, ILimit limit);
    ILimitedRate Create(IMeasure numerator, string name, ILimit limit);
    ILimitedRate Create(IMeasure numerator, Enum denominatorContext, ValueType denominatorQuantity, ILimit limit);
    ILimitedRate Create(IMeasure numerator, MeasureUnitCode denominatorCode, ILimit limit);
    ILimitedRate Create(IMeasure numerator, IMeasurement denominator, ILimit limit);
    ILimitedRate Create(IMeasure numerator, IDenominator denominator, ILimit limit);
    ILimitedRate Create(IRate rate, ILimit limit);
    ILimit CreateLimit(IBaseMeasure baseMeasure);
}
