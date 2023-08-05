namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IMeasureUnitType
{
    Type GetMeasureUnitType(MeasureUnitTypeCode? measureUnitTypeCode = null);
    MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum? measureUnit = null);
    bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, Enum? measureUnit = null);

    void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
}