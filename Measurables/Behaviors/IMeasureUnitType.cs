namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IMeasureUnitType
{
    MeasureUnitTypeCode GetMeasureUnitTypeCode();

    void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
}