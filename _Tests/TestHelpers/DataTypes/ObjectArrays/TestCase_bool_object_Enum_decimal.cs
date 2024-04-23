namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_bool_object_Enum_decimal(string TestCase, bool IsTrue, object Obj, Enum MeasureUnit, decimal Quantity) : TestCase_bool_object_Enum(TestCase, IsTrue, Obj, MeasureUnit)
{
    public override object[] ToObjectArray() => [TestCase, IsTrue, Obj, MeasureUnit, Quantity];
}
