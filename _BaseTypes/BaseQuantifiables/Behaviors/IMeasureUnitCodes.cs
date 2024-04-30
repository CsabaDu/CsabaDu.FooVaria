namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

public interface IMeasureUnitCodes : IMeasureUnitCode
{
    IEnumerable<MeasureUnitCode> GetMeasureUnitCodes();
    //bool IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode);
    void ValidateMeasureUnitCodes(IBaseQuantifiable? baseQuantifiable, string paramName);
}
