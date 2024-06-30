namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_TypeCode_object(string TestCase, TypeCode TypeCode, object Object) : TestCase_TypeCode(TestCase, TypeCode)
{
    public override object[] ToObjectArray() => [TestCase, TypeCode, Object];
}
