namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources.ObjectArrayItems;

public record MeasureUnitCode_IMeasurable_args(MeasureUnitCode MeasureUnitCode, IMeasurable Measurable) : MeasureUnitCode_arg(MeasureUnitCode)
{
    public override object[] ToObjectArray() => [MeasureUnitCode, Measurable];
}
