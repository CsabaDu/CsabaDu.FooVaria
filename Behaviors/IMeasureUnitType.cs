namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IMeasureUnitType
{
    MeasureUnitTypeCode GetMeasureUnitTypeCode();

    void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
}