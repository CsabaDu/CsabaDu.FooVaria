namespace CsabaDu.FooVaria.Rates.Types;

public interface ILimitedRate : IRate/*<ILimitedRate>*/, ILimiter<ILimitedRate, IMeasure>, ICommonBase<ILimitedRate>
{
    ILimit Limit { get; init; }

    ILimitedRate GetLimitedRate(IMeasure numerator, string name, decimal quantity, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, string name, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal quantity, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator denominator, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, ILimit limit);
    ILimitedRate GetLimitedRate(IRate rate, ILimit limit);
}

    //ILimitedRate GetLimitedRate(ILimitedRate other);
    //ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, decimal quantity, ILimit limit);
    //ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, ILimit limit);