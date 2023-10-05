namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IMeasureUnitType
{
    Type GetMeasureUnitType();
    Type GetMeasureUnitType(MeasureUnitTypeCode measureUnitTypeCode);
    MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum measureUnit);
    IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes();
    bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
    bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, Enum measureUnit);
    bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measurementUnitTypeCode);

    void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
}
