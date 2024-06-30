namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_MeasureUnitCode_IMeasurable(string TestCase, MeasureUnitCode MeasureUnitCode, IMeasurable Measurable) : TestCase_MeasureUnitCode(TestCase, MeasureUnitCode)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnitCode, Measurable];
}
