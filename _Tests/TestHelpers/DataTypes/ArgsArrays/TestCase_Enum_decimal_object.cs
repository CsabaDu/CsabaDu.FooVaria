namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_Enum_decimal_object(string TestCase, Enum MeasureUnit, decimal DefaultQuantity, object Obj) : TestCase_Enum_decimal(TestCase, MeasureUnit, DefaultQuantity)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, DefaultQuantity, Obj];
}
