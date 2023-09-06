

namespace CsabaDu.FooVaria.Measurables.Factories;

public interface ILimitFactory : IBaseMeasureFactory
{
    ILimit Create(string name, ValueType? quantity, LimitMode? limitMode);
    ILimit Create(Enum measureUnit, ValueType? quantity, LimitMode? limitMode);
    ILimit Create(Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity, LimitMode? limitMode);
    ILimit Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity, LimitMode? limitMode);
    ILimit Create(IMeasurement measurement, ValueType? quantity, LimitMode? limitMode);
    ILimit Create(IBaseMeasure baseMeasure, LimitMode? limitMode);
    ILimit Create(IDenominator denominator);
    ILimit Create(ILimit limit, LimitMode? limitMode);
}
