namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurement : IMeasurable, IExchangeRateCollection, IRateComponent, ICustomMeasurement, IProportional<IMeasurement, Enum>
{
    object MeasureUnit { get; init; }
    decimal ExchangeRate { get; init; }

    IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null);
    IMeasurement GetMeasurement(IMeasurement? other = null);
    IMeasurement GetMeasurement(IBaseMeasure baseMeasure);

    bool IsValidMeasureUnit(Enum measureUnit);
    void RestoreConstantMeasureUnits();
}
