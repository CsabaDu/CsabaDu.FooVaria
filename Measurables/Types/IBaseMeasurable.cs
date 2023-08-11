namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasurable : IMeasureUnitType, IDefaultMeasureUnit
{
    MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }

    Enum GetMeasureUnit();


    MeasureUnitTypeCode[] GetMeasureUnitTypeCodes();
    bool IsDefinedMeasureUnit(Enum measureUnit);

    void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null);
}
