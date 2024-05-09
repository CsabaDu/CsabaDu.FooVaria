namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseQuantifiables;

public sealed class MeasureUnitCodesObject : IMeasureUnitCodes
{
    public IEnumerable<MeasureUnitCode> MeasureUnitCodes {  private get; set; }
    public IEnumerable<MeasureUnitCode> GetMeasureUnitCodes() => MeasureUnitCodes;

    public void ValidateMeasureUnitCodes(IMeasureUnitCodes measureUnitCodes, string paramName)
    {
        IEnumerable<MeasureUnitCode> otherMeasureUnitCodes = NullChecked(measureUnitCodes, paramName).GetMeasureUnitCodes();

        foreach (MeasureUnitCode item in otherMeasureUnitCodes)
        {
            if (!MeasureUnitCodes.Contains(item)) throw InvalidMeasureUnitCodeEnumArgumentException(item, paramName);
        }
    }
}
