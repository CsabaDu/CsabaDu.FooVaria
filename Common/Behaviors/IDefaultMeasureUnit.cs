namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IDefaultMeasureUnit : IMeasureUnit
{
    Enum GetDefaultMeasureUnit();
    Enum GetDefaultMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode);
    IEnumerable<string> GetDefaultNames(MeasureUnitTypeCode? measureUnitTypeCode);
}
