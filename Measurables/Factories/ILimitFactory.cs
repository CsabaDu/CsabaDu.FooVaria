namespace CsabaDu.FooVaria.Measurables.Factories;

public interface ILimitFactory : IBaseMeasureFactory
{
    ILimit Create(Enum measureUnit, decimal? exchangeRate, ValueType? quantity, LimitMode? limitMode);
    ILimit Create(IMeasurement measurement, ValueType? quantity, LimitMode? limitMode);
    ILimit Create(IBaseMeasure baseMeasure, LimitMode? limitMode);
    ILimit Create(IDenominator denominator);
    ILimit Create(ILimit other);
}
