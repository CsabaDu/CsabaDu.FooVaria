namespace CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Types;

public interface IBaseMeasurement : IMeasurable, IExchangeRateCollection, IProportional<IBaseMeasurement>/*, IMeasureUnit<Enum>*/, IExchangeable<Enum>, INamed
{
    IBaseMeasurement? GetBaseMeasurement(Enum context);
}
