namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ICustomMeasureUnit
{
    bool IsCustomMeasureUnit(Enum measureUnit);
    bool IsCustomMeasureUnitTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null);
    bool TryAddCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string? customName = null);
    IEnumerable<T> GetInvalidCustomMeasureUnits<T>() where T : struct, Enum;
    IEnumerable<Enum> GetInvalidCustomMeasureUnits(MeasureUnitTypeCode? measureUnitTypeCode = null);
    IEnumerable<MeasureUnitTypeCode> GetCustomMeasureUnitTypeCodes();



    void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measurementUnitTypeCode);
    void ValidateExchangeRate(decimal? exchangeRate, Enum? measureUnit = null);
}
