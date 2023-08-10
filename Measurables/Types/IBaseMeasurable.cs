namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasurable : IMeasureUnitType
{
    MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }

    Enum GetMeasureUnit();

    Enum GetDefaultMeasureUnit(MeasureUnitTypeCode? measureUnitTypeCode = null);
    string[] GetDefaultNames(MeasureUnitTypeCode? measureUnitTypeCode = null);
    string GetDefaultName(Enum? measureUnit = null);

    MeasureUnitTypeCode[] GetMeasureUnitTypeCodes();
    bool IsDefinedMeasureUnit(Enum measureUnit);

    void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null);
}
