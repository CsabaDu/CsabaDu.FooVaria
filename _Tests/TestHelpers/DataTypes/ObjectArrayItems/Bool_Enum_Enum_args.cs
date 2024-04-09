namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record bool_Enum_Enum_args(bool IsTrue, Enum MeasureUnit, Enum Context) : bool_Enum_args(IsTrue, MeasureUnit)
{
    public override object[] ToObjectArray() => [IsTrue, MeasureUnit, Context];
}
