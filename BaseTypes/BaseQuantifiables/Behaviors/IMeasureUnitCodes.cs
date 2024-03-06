namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

public interface IMeasureUnitCodes/* : ILimitable*/
{
    IEnumerable<MeasureUnitCode> GetMeasureUnitCodes();
    bool IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode);
    void ValidateMeasureUnitCodes(IBaseQuantifiable? baseQuantifiable, string paramName);
}
