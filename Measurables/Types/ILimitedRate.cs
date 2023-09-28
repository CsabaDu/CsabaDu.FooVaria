namespace CsabaDu.FooVaria.Measurables.Types;

public interface ILimitedRate : IRate, ILimiter<ILimitedRate, IMeasure>
{
    ILimit Limit { get; init; }

    ILimitedRate GetLimitedRate(IMeasure numerator, string name, decimal quantity, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, string name, ILimit limit);

    ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal quantity, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, ILimit limit);

    ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customNam, decimal quantity, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customNam, ILimit limit);

    ILimitedRate GetLimitedRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal quantity, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ILimit limit);

    ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, decimal quantity, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, ILimit limit);

    ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator denominator, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, ILimit limit);

    ILimitedRate GetLimitedRate(IRate rate, ILimit limit);
    ILimitedRate GetLimitedRate(ILimitedRate other);

    //ILimitedRateFactory GetFactory();
    ILimit GetOrCreateLimit(ILimit limit);
}
