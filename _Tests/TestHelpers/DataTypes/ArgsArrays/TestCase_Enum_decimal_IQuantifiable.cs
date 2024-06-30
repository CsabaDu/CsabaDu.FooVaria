namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_Enum_decimal_IQuantifiable(string TestCase, Enum MeasureUnit, decimal DefaultQuantity, IQuantifiable Quantifiable) : TestCase_Enum_decimal(TestCase, MeasureUnit, DefaultQuantity)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, DefaultQuantity, Quantifiable];
}
