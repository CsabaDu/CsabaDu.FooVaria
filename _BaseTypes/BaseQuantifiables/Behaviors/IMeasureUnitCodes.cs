namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

public interface IMeasureUnitCodes
{
    IEnumerable<MeasureUnitCode> GetMeasureUnitCodes();

    void ValidateMeasureUnitCodes(IMeasureUnitCodes? measureUnitCodes, string paramName);
}
