namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Measurables;

public abstract class MeasureUnitCodeObject : IMeasureUnitCode
{
    public MeasureUnitCode MeasureUnitCode { private get; set; }

    public MeasureUnitCode GetMeasureUnitCode() => MeasureUnitCode;

    public bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return measureUnitCode == MeasureUnitCode;
    }

    public void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, [DisallowNull] string paramName)
    {
        if (HasMeasureUnitCode(measureUnitCode)) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode);
    }
}
