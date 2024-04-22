namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record RoundingMode_args(string Case, RoundingMode RoundingMode) : ObjectArray(Case)
{
    public override object[] ToObjectArray() => [Case, RoundingMode];
}
