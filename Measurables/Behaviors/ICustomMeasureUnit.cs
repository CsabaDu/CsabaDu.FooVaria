namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ICustomMeasureUnit : IMeasureUnit
{
    bool IsCustomMeasureUnit(Enum measureUnit);
    bool TryAddCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string? customName = null);
    bool TryAddCustomMeasureUnitName(Enum measureUnit, string customName);
    IDictionary<Enum, string> GetCustomMeasureUnitNameCollection(MeasureUnitTypeCode? measureUnitTypeCode = null);

    void ValidateExchangeRate(decimal? exchangeRate, Enum? measureUnit = null);
}
