namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Enum_LimitMode_args(string Case, Enum MeasureUnit, LimitMode? LimitMode) : Args_Enum(Case, MeasureUnit)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, LimitMode];
}
