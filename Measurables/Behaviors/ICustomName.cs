namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ICustomName
{
    string? GetCustomName(Enum? measureUnit = null);
    bool TrySetCustomName(Enum measureUnit, string? customName);
    IDictionary<Enum, string> GetCustomNameCollection(MeasureUnitTypeCode? measureUnitTypeCode = null);

    void SetCustomName(Enum measureUnit, string? customName);
    void SetOrReplaceCustomName(string customName);
    void ValidateCustomName(string? customName);
}
