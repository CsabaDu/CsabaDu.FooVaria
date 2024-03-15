namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources.ObjectArrayItems;

public record MeasureUnitCode_IMeasurable_bool_args(MeasureUnitCode MeasureUnitCode, IMeasurable Measurable, bool IsTrue) : MeasureUnitCode_IMeasurable_args(MeasureUnitCode, Measurable)
{
    public override object[] ToObjectArray() => [MeasureUnitCode, Measurable, IsTrue];
}
