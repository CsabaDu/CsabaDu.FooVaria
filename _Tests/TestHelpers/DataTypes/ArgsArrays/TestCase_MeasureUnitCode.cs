namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_MeasureUnitCode(string TestCase, MeasureUnitCode MeasureUnitCode) : ArgsArray(TestCase)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnitCode];
}
