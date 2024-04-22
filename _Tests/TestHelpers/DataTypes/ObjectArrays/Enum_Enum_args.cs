namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Enum_Enum_args(string Case, Enum MeasureUnit, Enum Context) : Enum_args(Case, MeasureUnit)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, Context];
}
