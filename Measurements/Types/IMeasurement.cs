namespace CsabaDu.FooVaria.Measurements.Types;

public interface IMeasurement : IBaseMeasurement, IMeasureUnitCollection, ICustomNameCollection, IDefaultMeasurable<IMeasurement>
{
    object MeasureUnit { get; init; }

    IMeasurement GetMeasurement(Enum measureUnit);
    IMeasurement GetMeasurement(IMeasurement other);
    IMeasurement? GetMeasurement(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate);
    IMeasurement? GetMeasurement(Enum measureUnit, decimal exchangeRate, string customName);
    IMeasurement GetMeasurement(string name);
    bool TryGetMeasurement(decimal exchangeRate, [NotNullWhen(true)] out IMeasurement? measurement);
}

