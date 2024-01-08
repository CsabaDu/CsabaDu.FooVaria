namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasurement : IMeasurable, IExchangeRateCollection, Behaviors.IProportional<IBaseMeasurement>, IMeasureUnit<Enum>, IExchangeable<Enum>/*, IExchangeRate*/, INamed
{
    IBaseMeasurement? GetBaseMeasurement(object context);
}
