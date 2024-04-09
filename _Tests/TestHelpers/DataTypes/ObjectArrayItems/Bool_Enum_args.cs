namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record bool_Enum_args(bool IsTrue, Enum MeasureUnit) : bool_arg(IsTrue)
{
    public override object[] ToObjectArray() => [IsTrue, MeasureUnit];
}
