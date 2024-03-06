namespace CsabaDu.FooVaria.BaseTypes.Measurables.Behaviors;

public interface IDefaultMeasureUnit
{
    Enum GetDefaultMeasureUnit();
    IEnumerable<string> GetDefaultMeasureUnitNames();

    void ValidateMeasureUnit(Enum? measureUnit, [DisallowNull] string paramName);
}
