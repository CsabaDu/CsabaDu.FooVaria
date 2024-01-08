namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IMeasureUnitType
{
    Type GetMeasureUnitType();
    IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes();
    bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
    bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);

    void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, string paramName);
}
