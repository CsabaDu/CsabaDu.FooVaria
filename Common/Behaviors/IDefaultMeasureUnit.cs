namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IDefaultMeasureUnit : IMeasureUnit
{
    Enum GetDefaultMeasureUnit();
    IEnumerable<string> GetDefaultNames();
}
