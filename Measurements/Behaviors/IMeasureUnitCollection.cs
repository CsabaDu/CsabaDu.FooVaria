namespace CsabaDu.FooVaria.Measurements.Behaviors;

public interface IMeasureUnitCollection
{
    Enum? GetMeasureUnit(string name);
    string GetDefaultName();
    string GetDefaultName(Enum measureUnit);

    bool TryGetMeasureUnit(MeasureUnitCode measureUnitCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit);
    bool TryGetMeasureUnit(string name, [NotNullWhen(true)] out Enum? measureUnit);

    IDictionary<string, object> GetMeasureUnitCollection(MeasureUnitCode measureUnitCode);
    IDictionary<string, object> GetMeasureUnitCollection();
    IEnumerable<object> GetValidMeasureUnits(MeasureUnitCode measureUnitCode);

}
