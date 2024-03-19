namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_LimitMode_arg(Enum MeasureUnit, LimitMode? LimitMode) : Enum_arg(MeasureUnit)
{
    public override object[] ToObjectArray() => [MeasureUnit, LimitMode];
}
