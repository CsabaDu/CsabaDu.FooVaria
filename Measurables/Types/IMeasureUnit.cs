namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasureUnit : IMeasureUnitType
{
    MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }

    Enum GetMeasureUnit();
    Enum GetDefaultMeasureUnit(MeasureUnitTypeCode? measureUnitTypeCode = null);
    string[] GetMeasureUnitNames(MeasureUnitTypeCode? measureUnitTypeCode = null);
    bool IsDefinedMeasureUnit(Enum measureUnit);

    void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null);
}

    //Enum? GetDefinedMeasureUnit(Enum measureUnit);