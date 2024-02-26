namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

public interface IMeasureUnitTypeCodes
{
    IEnumerable<MeasureUnitCode> GetMeasureUnitCodes();
    bool IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode);
}