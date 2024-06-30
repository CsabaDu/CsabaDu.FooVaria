namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_Enum_decimal_bool(string TestCase, Enum MeasureUnit, decimal DefaultQuantity, bool IsTrue) : TestCase_Enum_decimal(TestCase, MeasureUnit, DefaultQuantity)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, DefaultQuantity, IsTrue];
}
