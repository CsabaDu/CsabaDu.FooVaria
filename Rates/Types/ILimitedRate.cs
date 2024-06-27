using CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

namespace CsabaDu.FooVaria.Rates.Types;

public interface ILimitedRate : IRate, ILimiter<ILimitedRate, IBaseMeasure>, IDeepCopy<ILimitedRate>, IConcreteFactory<ILimitedRateFactory>
{
    //ILimitedRateFactory Factory { get; init; }
    ILimit Limit { get; init; }

    ILimitedRate GetLimitedRate(IMeasure numerator, string name, ValueType quantity, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, string name, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, Enum denominatorMeasureUnit, ValueType denominatorQuantity, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement denominatorMeasurement, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, MeasureUnitCode denominatorCode, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator denominator, ILimit limit);
    ILimitedRate GetLimitedRate(IMeasure numerator, ILimit limit);
    ILimitedRate GetLimitedRate(IRate rate, ILimit limit);
}
