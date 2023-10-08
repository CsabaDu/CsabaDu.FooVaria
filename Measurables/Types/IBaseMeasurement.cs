namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasurement : IMeasurable, IMeasureUnitCollection, IExchangeRateCollection, ICustomNameCollection, IRateComponentType
{
    string GetName();
}
