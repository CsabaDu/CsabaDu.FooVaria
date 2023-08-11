namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ICustomMeasureUnit : ICustomMeasureUnitType
{
    bool IsCustomMeasureUnit(Enum measureUnit);
    bool TryAddCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string? customName = null);
    IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitTypeCode? customMeasureUnitTypeCode = null);
    IEnumerable<T> GetNotUsedCustomMeasureUnits<T>() where T : struct, Enum;
}
