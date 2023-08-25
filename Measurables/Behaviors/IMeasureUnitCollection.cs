namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IMeasureUnitCollection
{
    Enum? GetMeasureUnit(string name);

    bool IsValidMeasureUnit(Enum measureUnit, decimal? exchangeRate = null);
    bool TryGetMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit);
    bool TryGetMeasureUnit(string name, [NotNullWhen(true)] out Enum? measureUnit);

    IDictionary<string, Enum> GetMeasureUnitCollection(MeasureUnitTypeCode? measureUnitTypeCode = null);
    IEnumerable<Enum> GetValidMeasureUnits(MeasureUnitTypeCode? measureUnitTypeCode = null);
}
