namespace CsabaDu.FooVaria.RateComponents.Factories;

public interface ILimitFactory : IRateComponentFactory, IMeasurableFactory<ILimit>
{
    ILimit Create(string name, ValueType quantity, LimitMode limitMode);
    ILimit Create(Enum measureUnit, ValueType quantity, LimitMode limitMode);
    ILimit Create(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity, LimitMode limitMode);
    ILimit Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity, LimitMode limitMode);
    ILimit Create(IMeasurement measurement, ValueType quantity, LimitMode limitMode);
    ILimit Create(IRateComponent baseMeasure, LimitMode limitMode);
    ILimit Create(ILimit limit);
    ILimit Create(ILimit limit, ValueType quantity);
}
