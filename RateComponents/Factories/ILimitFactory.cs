namespace CsabaDu.FooVaria.RateComponents.Factories;

public interface ILimitFactory : IRateComponentFactory<ILimit>, IBaseMeasureFactory<ILimit, ulong>, IFactory<ILimit>
{
    ILimit Create(string name, ValueType quantity, LimitMode limitMode);
    ILimit Create(Enum measureUnit, ValueType quantity, LimitMode limitMode);
    ILimit? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, LimitMode limitMode);
    ILimit? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity, LimitMode limitMode);
    ILimit Create(IMeasurement measurement, ulong quantity, LimitMode limitMode);
    ILimit Create(IBaseMeasure baseMeasure, LimitMode limitMode);
    ILimit Create(ILimit limit, ValueType quantity);
}
