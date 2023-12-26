namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseMeasurement : IMeasurable, IExchangeRateCollection, IProportional<IBaseMeasurement>, IMeasureUnit<Enum>, IExchangeable<Enum>, IExchangeRate, INamed
{
    decimal ExchangeRate { get; init; }
}
