namespace CsabaDu.FooVaria.Rates.Factories;

public interface ILimitedRateFactory : IRateFactory, IFactory<ILimitedRate>
{
    ILimitFactory LimitFactory { get; init; }

    ILimitedRate Creat(IMeasure numerator, string name, ValueType quantity, ILimit limit);
    ILimitedRate Creat(IMeasure numerator, Enum denominatorMeasureUnit, ValueType quantity, ILimit limit);
    ILimitedRate Creat(IMeasure numerator, string name, ILimit limit);
    ILimitedRate Creat(IMeasure numerator, Enum denominatorMeasureUnit, ILimit limit);
    ILimitedRate Creat(IMeasure numerator, IMeasurement measurement, ILimit limit);
    ILimitedRate Creat(IMeasure numerator, IMeasurement measurement, ValueType quantity, ILimit limit);
    ILimitedRate Creat(IMeasure numerator, IDenominator denominator, ILimit limit);
    ILimitedRate Creat(IRate rate, ILimit? limit);
    ILimit CreateLimit(IDenominator denominator);
}
