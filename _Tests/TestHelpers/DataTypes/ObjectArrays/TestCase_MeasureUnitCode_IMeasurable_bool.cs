namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_MeasureUnitCode_IMeasurable_bool(string TestCase, MeasureUnitCode MeasureUnitCode, IMeasurable Measurable, bool IsTrue) : TestCase_MeasureUnitCode_IMeasurable(TestCase, MeasureUnitCode, Measurable)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnitCode, Measurable, IsTrue];
}
