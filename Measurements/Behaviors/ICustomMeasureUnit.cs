using CsabaDu.FooVaria.Measurables.Enums;

namespace CsabaDu.FooVaria.Measurements.Behaviors;

public interface ICustomMeasureUnit
{
    IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitCode measureUnitCode);
    IEnumerable<Enum> GetNotUsedCustomMeasureUnits();
    //bool IsCustomMeasureUnit(Enum measureUnit);
    bool TrySetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName);
    bool TrySetCustomMeasureUnit(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate);

    void SetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName);
    void SetOrReplaceCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName);

    void SetCustomMeasureUnit(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate);
}
