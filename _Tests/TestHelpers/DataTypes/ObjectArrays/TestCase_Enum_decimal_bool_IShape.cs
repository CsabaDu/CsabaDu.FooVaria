namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_Enum_decimal_bool_IShape(string TestCase, Enum MeasureUnit, decimal DefaultQuantity, bool IsTrue, IShape Shape) : TestCase_Enum_decimal_bool(TestCase, MeasureUnit, DefaultQuantity, IsTrue)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, DefaultQuantity, IsTrue, Shape];
    }
}
