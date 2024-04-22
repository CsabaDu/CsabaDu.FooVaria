namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_TypeCode(string TestCase, TypeCode TypeCode) : ObjectArray(TestCase)
{
    public override object[] ToObjectArray() => [TestCase, TypeCode];
}
