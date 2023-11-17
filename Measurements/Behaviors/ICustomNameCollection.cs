namespace CsabaDu.FooVaria.Measurements.Behaviors;

public interface ICustomNameCollection
{
    string? GetCustomName(Enum measureUnit);
    string? GetCustomName();
    bool TrySetCustomName(Enum measureUnit, string customName);
    IDictionary<object, string> GetCustomNameCollection(MeasureUnitTypeCode measureUnitTypeCode);
    IDictionary<object, string> GetCustomNameCollection();

    void SetCustomName(Enum measureUnit, string customName);
    void SetOrReplaceCustomName(string customName);
    void ValidateCustomName(string? customName);
}
