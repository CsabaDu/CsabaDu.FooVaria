namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_Enum_decimal(string TestCase, Enum MeasureUnit, decimal DefaultQuantity) : TestCase_Enum(TestCase, MeasureUnit)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, DefaultQuantity];
}
