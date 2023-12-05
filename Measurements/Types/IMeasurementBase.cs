namespace CsabaDu.FooVaria.Measurements.Types;

public interface IMeasurementBase : IMeasurable, IExchangeRateCollection, IProportional<IMeasurementBase>, IMeasureUnit<Enum>, IExchangeable<Enum>, IExchangeRate, INamed
{
    decimal ExchangeRate { get; init; }
}

