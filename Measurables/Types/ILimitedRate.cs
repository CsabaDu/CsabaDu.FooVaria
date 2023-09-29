namespace CsabaDu.FooVaria.Measurables.Types;

public interface ILimitedRate : IRate, ILimiter<IFlatRate, IMeasure>
{
    ILimit Limit { get; init; }

    IFlatRate GetLimitedRate(IMeasure numerator, string name, decimal quantity, ILimit limit);
    IFlatRate GetLimitedRate(IMeasure numerator, string name, ILimit limit);

    IFlatRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal quantity, ILimit limit);
    IFlatRate GetLimitedRate(IMeasure numerator, Enum measureUnit, ILimit limit);

    IFlatRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customNam, decimal quantity, ILimit limit);
    IFlatRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customNam, ILimit limit);

    IFlatRate GetLimitedRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal quantity, ILimit limit);
    IFlatRate GetLimitedRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ILimit limit);

    IFlatRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, decimal quantity, ILimit limit);
    IFlatRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, ILimit limit);

    IFlatRate GetLimitedRate(IMeasure numerator, IDenominator denominator, ILimit limit);
    IFlatRate GetLimitedRate(IMeasure numerator, ILimit limit);

    IFlatRate GetLimitedRate(IRate rate, ILimit limit);
    IFlatRate GetLimitedRate(IFlatRate other);

    //ILimitedRateFactory GetFactory();
    ILimit GetOrCreateLimit(ILimit limit);
}
