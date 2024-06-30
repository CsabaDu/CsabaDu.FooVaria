namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_Enum_decimal_object_TypeCode(string TestCase, Enum MeasureUnit, decimal DefaultQuantity, object Obj, TypeCode TypeCode) : TestCase_Enum_decimal_object(TestCase, MeasureUnit, DefaultQuantity, Obj)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, DefaultQuantity, Obj, TypeCode];
}
