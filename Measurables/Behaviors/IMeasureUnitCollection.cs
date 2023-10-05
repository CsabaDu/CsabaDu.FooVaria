namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IMeasureUnitCollection
{
    Enum? GetMeasureUnit(string name);
    string GetDefaultName();
    string GetDefaultName(Enum measureUnit);

    bool IsValidMeasureUnit(Enum? measureUnit);
    bool TryGetMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit);
    bool TryGetMeasureUnit(string name, [NotNullWhen(true)] out Enum? measureUnit);

    IDictionary<string, object> GetMeasureUnitCollection(MeasureUnitTypeCode measureUnitTypeCode);
    IDictionary<string, object> GetMeasureUnitCollection();
    IEnumerable<object> GetValidMeasureUnits(MeasureUnitTypeCode measureUnitTypeCode);
}
