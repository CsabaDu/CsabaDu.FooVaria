namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_bool_Enum_ValueType_IQuantifiable(string TestCase, bool IsTrue, Enum MeasureUnit, ValueType Quantity, IQuantifiable Quantifiable) : TestCase_bool_Enum_ValueType(TestCase, IsTrue, MeasureUnit, Quantity)
{
    public override object[] ToObjectArray() => [TestCase, IsTrue, MeasureUnit, Quantity, Quantifiable];
}
