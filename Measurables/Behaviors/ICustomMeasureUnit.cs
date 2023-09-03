namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ICustomMeasureUnit : ICustomMeasureUnitType
{
    IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitTypeCode? measureUnitTypeCode = null);
    bool IsCustomMeasureUnit(Enum measureUnit);
    bool TrySetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName);
    bool TrySetCustomMeasureUnit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);

    void SetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName);
    void SetCustomMeasureUnit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
}
