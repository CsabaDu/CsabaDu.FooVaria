namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record string_args(string Case, string ParamName) : ObjectArray(Case)
{
    public override object[] ToObjectArray() => [Case, ParamName];
}
