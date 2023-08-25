namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IDefaultMeasureUnit : IMeasureUnit
{
    Enum GetDefaultMeasureUnit(MeasureUnitTypeCode? measureUnitTypeCode = null);
    IEnumerable<string> GetDefaultNames(MeasureUnitTypeCode? measureUnitTypeCode = null);
    string GetDefaultName(Enum? measureUnit = null);
}
