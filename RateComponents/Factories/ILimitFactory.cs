namespace CsabaDu.FooVaria.RateComponents.Factories;

public interface ILimitFactory : IRateComponentFactory, IDefaultRateComponentFactory<ILimit>
{
    ILimit Create(string name, ValueType quantity, LimitMode limitMode);
    ILimit Create(Enum measureUnit, ValueType quantity, LimitMode limitMode);
    ILimit? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, LimitMode limitMode);
    ILimit? Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity, LimitMode limitMode);
    ILimit Create(IMeasurement measurement, ValueType quantity, LimitMode limitMode);
    ILimit Create(IRateComponent rateComponent, LimitMode limitMode);
    ILimit Create(ILimit limit, ValueType quantity);
}
