namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_Enum_ISpreadMeasure_IQuantifiable(string TestCase, Enum MeasureUnit, ISpreadMeasure SpreadMeasure, IQuantifiable Quantifiable) : TestCase_Enum_ISpreadMeasure(TestCase, MeasureUnit, SpreadMeasure)
{
    public override object[] ToObjectArray() => [TestCase, MeasureUnit, SpreadMeasure, Quantifiable];
}

