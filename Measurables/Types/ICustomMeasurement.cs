namespace CsabaDu.FooVaria.Measurables.Types;

public interface ICustomMeasurement : IBaseMeasurable, ICustomMeasureUnit
{
    ICustomMeasurement GetNextCustomMeasurement(MeasureUnitTypeCode customMeasureUnitTypeCode, decimal exchangeRate, string? customName = null);

    void InitiateCustomExchangeRates(MeasureUnitTypeCode customMeasureUnitTypeCode, params decimal[] exchangeRates);
}
