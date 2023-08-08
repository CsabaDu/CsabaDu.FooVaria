namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ICustomMeasureUnit : IMeasureUnit
{
    bool IsCustomMeasureUnit(Enum measureUnit);
    bool TryAddCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string? customName = null);

    void ValidateExchangeRate(decimal? exchangeRate, Enum? measureUnit = null);
}
