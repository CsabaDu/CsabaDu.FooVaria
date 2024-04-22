namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_bool_Enum_IBaseMeasurement(string Case, bool IsTrue, Enum MeasureUnit, IBaseMeasurement BaseMeasurement) : Args_bool_Enum(Case, IsTrue, MeasureUnit)
{
    public override object[] ToObjectArray() => [Case, IsTrue, MeasureUnit, BaseMeasurement];
}
