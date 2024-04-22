namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_string(string TestCase, string ParamName) : ObjectArray(TestCase)
{
    public override object[] ToObjectArray() => [TestCase, ParamName];
}
