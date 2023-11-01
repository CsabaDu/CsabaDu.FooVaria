namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasurement : IMeasurable, IMeasureUnitCollection, IExchangeRateCollection, ICustomNameCollection, IRateComponentType, IMeasureUnit<Enum>, IExchangeRate
{
    string GetName();
}
