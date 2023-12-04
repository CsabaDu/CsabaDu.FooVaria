namespace CsabaDu.FooVaria.Measurements.Behaviors;

public interface ICustomMeasureUnit
{
    IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitTypeCode measureUnitTypeCode);
    IEnumerable<Enum> GetNotUsedCustomMeasureUnits();
    //bool IsCustomMeasureUnit(Enum measureUnit);
    bool TrySetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName);
    bool TrySetCustomMeasureUnit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);

    void SetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName);
    void SetOrReplaceCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName);

    void SetCustomMeasureUnit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
}
