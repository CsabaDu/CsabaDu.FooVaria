namespace CsabaDu.FooVaria.Measurables.Types;

public interface ICustomName
{
    string? CustomName { get; }

    Enum? GetMeasureUnit(string customName);
    bool TryAddCustomName(Enum measureUnit, string? customName);
    bool TryGetMeasureUnit(string customName, [NotNullWhen(true)] out Enum? measureUnit);
    IDictionary<Enum, string> GetCustomNameCollection(MeasureUnitTypeCode? measureUnitTypeCode = null);

    void ValidateCustomName(string? customName);
}
