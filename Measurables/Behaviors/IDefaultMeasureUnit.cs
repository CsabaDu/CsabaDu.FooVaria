namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IDefaultMeasureUnit
{
    Enum GetDefaultMeasureUnit(MeasureUnitTypeCode? measureUnitTypeCode = null);
    string[] GetDefaultNames(MeasureUnitTypeCode? measureUnitTypeCode = null);
    string GetDefaultName(Enum? measureUnit = null);
}
