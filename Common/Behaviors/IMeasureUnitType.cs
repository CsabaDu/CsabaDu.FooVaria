namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IMeasureUnitType
{
    Type GetMeasureUnitType();
    IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes();
    bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
    bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measurementUnitTypeCode);

    void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
}

    //Type GetMeasureUnitType(MeasureUnitTypeCode measureUnitTypeCode);
    //MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum measureUnit);
    //bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, Enum measureUnit);