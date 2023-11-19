namespace CsabaDu.FooVaria.Measurements.Types;

public interface IBaseMeasurement : IMeasurable, IMeasureUnitCollection, IExchangeRateCollection, ICustomNameCollection/*, IRateComponent*/, IMeasureUnit<Enum>, IExchangeRate
{
    string GetName();
}
