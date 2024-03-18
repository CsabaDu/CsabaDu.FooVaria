namespace CsabaDu.FooVaria.Measurements.Behaviors;

public interface ICustomNameCollection
{
    string? GetCustomName();
    bool TrySetCustomName(string? customName);
    IDictionary<object, string> GetCustomNameCollection();

    void SetCustomName(string customName);
    void SetOrReplaceCustomName(string customName);
}

//string? GetCustomName(Enum measureUnit);
//void ValidateCustomName(string? customName);