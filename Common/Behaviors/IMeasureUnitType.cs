namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IMeasureUnitType
{
    Type GetMeasureUnitType(); // TODO
    Type GetMeasureUnitType(MeasureUnitTypeCode measureUnitTypeCode); // TODO
    MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum measureUnit); // TODO
    IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes();
    bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);  // TODO
    bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, Enum measureUnit); // TODO

    void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
}
