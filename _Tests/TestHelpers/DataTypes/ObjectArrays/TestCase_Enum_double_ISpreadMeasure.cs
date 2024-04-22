namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_Enum_double_ISpreadMeasure(string TestCase, Enum MeasureUnit, double Quantity, ISpreadMeasure SpreadMeasure) : TestCase_Enum_double(TestCase, MeasureUnit, Quantity)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, Quantity, SpreadMeasure];
}

