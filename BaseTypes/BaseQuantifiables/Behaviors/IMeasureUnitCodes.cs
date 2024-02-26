namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

public interface IMeasureUnitCodes
{
    IEnumerable<MeasureUnitCode> GetMeasureUnitCodes();
    bool IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode);
}
