namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IDefaultMeasureUnit
{
    Enum GetDefaultMeasureUnit();
    IEnumerable<string> GetDefaultMeasureUnitNames();

    void ValidateMeasureUnit(Enum measureUnit, string paramName);
}
