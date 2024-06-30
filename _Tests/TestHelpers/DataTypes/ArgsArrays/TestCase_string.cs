namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_string(string TestCase, string ParamName) : ArgsArray(TestCase)
{
    public override object[] ToObjectArray() => [TestCase, ParamName];
}
