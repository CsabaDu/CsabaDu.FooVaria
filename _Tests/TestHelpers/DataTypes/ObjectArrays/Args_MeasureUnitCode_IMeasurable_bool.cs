namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_MeasureUnitCode_IMeasurable_bool(string Case, MeasureUnitCode MeasureUnitCode, IMeasurable Measurable, bool IsTrue) : Args_MeasureUnitCode_IMeasurable(Case, MeasureUnitCode, Measurable)
{
    public override object[] ToObjectArray() => [Case, MeasureUnitCode, Measurable, IsTrue];
}
