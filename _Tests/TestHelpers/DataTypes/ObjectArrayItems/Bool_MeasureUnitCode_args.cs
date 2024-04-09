namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record bool_MeasureUnitCode_args(bool IsTrue, MeasureUnitCode MeasureUnitCode) : bool_arg(IsTrue)
{
    public override object[] ToObjectArray() => [IsTrue, MeasureUnitCode];
}
