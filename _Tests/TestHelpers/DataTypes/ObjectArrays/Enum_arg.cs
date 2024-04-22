namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Enum_args(string Case, Enum MeasureUnit) : ObjectArray(Case)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit];
}
