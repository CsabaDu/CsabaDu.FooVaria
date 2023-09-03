namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IMeasureUnitType
{
    Type GetMeasureUnitType(MeasureUnitTypeCode? measureUnitTypeCode = null);
    MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum? measureUnit = null);
    IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes();
    bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, Enum? measureUnit = null);

    void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
}