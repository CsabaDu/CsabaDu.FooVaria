namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_Enum(string TestCase, Enum MeasureUnit) : ArgsArray(TestCase)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit];
}
