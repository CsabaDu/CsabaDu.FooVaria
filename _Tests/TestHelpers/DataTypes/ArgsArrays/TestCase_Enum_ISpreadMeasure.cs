namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_Enum_ISpreadMeasure(string TestCase, Enum MeasureUnit, ISpreadMeasure SpreadMeasure) : TestCase_Enum(TestCase, MeasureUnit)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, SpreadMeasure];
}
