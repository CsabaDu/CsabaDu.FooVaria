namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_bool_Enum_ValueType(string Case, bool IsTrue, Enum MeasureUnit, ValueType Quantity) : TestCase_bool_Enum(Case, IsTrue, MeasureUnit)
    {
        public override object[] ToObjectArray() => [TestCase, IsTrue, MeasureUnit, Quantity];
    }
}
