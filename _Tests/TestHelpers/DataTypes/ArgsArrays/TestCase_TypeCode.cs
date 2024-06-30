namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_TypeCode(string TestCase, TypeCode TypeCode) : ArgsArray(TestCase)
{
    public override object[] ToObjectArray() => [TestCase, TypeCode];
}
