namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_Enum_decimal_bool_IQuantifiable(string TestCase, Enum MeasureUnit, decimal DefaultQuantity, bool IsTrue, IQuantifiable Quantifiable) : TestCase_Enum_decimal_bool(TestCase, MeasureUnit, DefaultQuantity, IsTrue)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, DefaultQuantity, IsTrue, Quantifiable];
}
