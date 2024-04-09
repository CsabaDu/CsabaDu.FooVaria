namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record bool_Enum_IBaseMeasurement_args(bool IsTrue, Enum MeasureUnit, IBaseMeasurement BaseMeasurement) : bool_Enum_args(IsTrue, MeasureUnit)
{
    public override object[] ToObjectArray() => [IsTrue, MeasureUnit, BaseMeasurement];
}
