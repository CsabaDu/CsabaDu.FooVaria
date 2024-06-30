namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_bool_Enum_IBaseMeasurement(string Case, bool IsTrue, Enum MeasureUnit, IBaseMeasurement BaseMeasurement) : TestCase_bool_Enum(Case, IsTrue, MeasureUnit)
{
    public override object[] ToObjectArray() => [TestCase, IsTrue, MeasureUnit, BaseMeasurement];
}
