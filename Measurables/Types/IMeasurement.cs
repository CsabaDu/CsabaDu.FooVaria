namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurement : IMeasurable, IExchangeRateCollection, IRateComponent, ICustomName, ICustomMeasurement, IProportional<IMeasurement, Enum>
{
    object MeasureUnit { get; init; }
    decimal ExchangeRate { get; init; }

    IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null, string? customName = null);
    IMeasurement GetMeasurement(IMeasurement? other = null);
    IMeasurement GetMeasurement(IBaseMeasure baseMeasure);

    bool IsValidMeasureUnit(Enum measureUnit);
    bool TryGetMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName, [NotNullWhen(true)] out Enum? measureUnit);

    void RestoreConstantMeasureUnits();
}
