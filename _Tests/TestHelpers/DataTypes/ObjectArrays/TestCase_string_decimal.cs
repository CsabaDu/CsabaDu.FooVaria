namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_string_decimal(string TestCase, string ParamName, decimal DecimalQuantity) : TestCase_string(TestCase, ParamName)
{
    public override object[] ToObjectArray() => [TestCase, ParamName, DecimalQuantity];
}
