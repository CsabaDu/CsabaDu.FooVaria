namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ICustomName
{
    string? GetCustomName(Enum? measureUnit = null);
    bool TryAddCustomName(Enum measureUnit, string? customName);
    IDictionary<Enum, string> GetCustomNameCollection(MeasureUnitTypeCode? measureUnitTypeCode = null);

    void AddOrReplaceCustomName(string customName);
    void ValidateCustomName(string? customName);
}
