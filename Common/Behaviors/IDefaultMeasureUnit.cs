namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IDefaultMeasureUnit : IValidMeasureUnit
{
    Enum GetDefaultMeasureUnit();
    IEnumerable<string> GetDefaultNames();
}
