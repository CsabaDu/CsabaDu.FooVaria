namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ICustomMeasureUnit : ICustomMeasureUnitType
{
    bool IsCustomMeasureUnit(Enum measureUnit);
    bool TrySetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate/*, string? customName = null*/);
    IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitTypeCode? measureUnitTypeCode = null);

    void SetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate);
}
