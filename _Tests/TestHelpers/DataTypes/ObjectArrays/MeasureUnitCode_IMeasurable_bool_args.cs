namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record MeasureUnitCode_IMeasurable_bool_args(string Case, MeasureUnitCode MeasureUnitCode, IMeasurable Measurable, bool IsTrue) : MeasureUnitCode_IMeasurable_args(Case, MeasureUnitCode, Measurable)
{
    public override object[] ToObjectArray() => [Case, MeasureUnitCode, Measurable, IsTrue];
}
