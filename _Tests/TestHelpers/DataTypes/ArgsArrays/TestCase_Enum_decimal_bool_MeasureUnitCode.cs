namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_Enum_decimal_bool_MeasureUnitCode(string TestCase, Enum MeasureUnit, decimal DefaultQuantity, bool IsTrue, MeasureUnitCode MeasureUnitCode) : TestCase_Enum_decimal_bool(TestCase, MeasureUnit, DefaultQuantity, IsTrue)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, DefaultQuantity, IsTrue, MeasureUnitCode];
    }
}
