namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ICustomMeasureUnit
{
    bool IsCustomMeasureUnit(Enum measureUnit);
    bool TryAddCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string? customMeasureUnitName = null);
    bool TryAddCustomMeasureUnitName(Enum measureUnit, string customMeasureUnitName);
    IDictionary<Enum, string> GetCustomMeasureUnitNameCollection(MeasureUnitTypeCode? measureUnitTypeCode = null);

    void ValidateExchangeRate(decimal? exchangeRate, Enum? measureUnit = null);
}
