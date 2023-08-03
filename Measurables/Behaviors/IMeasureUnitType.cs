namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IMeasureUnitType
{
    MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum? measureUnit = null);
    bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);

    void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
}