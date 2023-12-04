namespace CsabaDu.FooVaria.Measurements.Types
{
    public interface IMeasurementBase : IMeasurable, IExchangeRateCollection, IProportional<IMeasurementBase>, IMeasureUnit<Enum>, IExchangeable<Enum>, IExchangeRate, INamed
    {
        decimal ExchangeRate { get; init; }
    }

    public interface IMeasurement : IMeasurementBase, IMeasureUnitCollection, ICustomNameCollection, IDefaultMeasurable<IMeasurement>
    {
        object MeasureUnit { get; init; }

        IMeasurement GetMeasurement(Enum measureUnit);
        IMeasurement GetMeasurement(IMeasurement other);
        IMeasurement GetMeasurement(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
        IMeasurement GetMeasurement(Enum measureUnit, decimal exchangeRate, string customName);
        IMeasurement GetMeasurement(string name);
        bool TryGetMeasurement(decimal exchangeRate, [NotNullWhen(true)] out IMeasurement? measurement);
    }

    public interface IConstantMeasurement : IMeasurement
    {

    }

    public interface ICustomMeasurement : IMeasurement, ICustomMeasureUnitTypeCode, ICustomMeasureUnit, ICustomMExchangeRates
    {

        bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement);
        bool TrySetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName);
    }
}

