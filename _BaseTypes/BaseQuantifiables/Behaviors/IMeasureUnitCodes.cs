namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

public interface IMeasureUnitCodes
{
    IEnumerable<MeasureUnitCode> GetMeasureUnitCodes();
    void ValidateMeasureUnitCodes(IBaseQuantifiable? baseQuantifiable, string paramName);
}
