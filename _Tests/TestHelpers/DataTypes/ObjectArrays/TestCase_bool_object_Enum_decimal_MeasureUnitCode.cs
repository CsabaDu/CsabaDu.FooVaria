namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_bool_object_Enum_decimal_MeasureUnitCode(string TestCase, bool IsTrue, object Obj, Enum MeasureUnit, decimal Quantity, MeasureUnitCode MeasureUnitCode) : TestCase_bool_object_Enum_decimal(TestCase, IsTrue, Obj, MeasureUnit, Quantity)
    {
        public override object[] ToObjectArray() => [TestCase, IsTrue, Obj, MeasureUnit, Quantity, MeasureUnitCode];
    }
}