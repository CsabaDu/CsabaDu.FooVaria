namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasurement : IMeasurable, IExchangeRateCollection, IProportional<IBaseMeasurement>, IMeasureUnit<Enum>, IExchangeable<Enum>, INamed
{
    IBaseMeasurement? GetBaseMeasurement(object context);
}
