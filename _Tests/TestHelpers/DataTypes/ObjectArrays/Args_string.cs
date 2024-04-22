namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_string(string Case, string ParamName) : ObjectArray(Case)
{
    public override object[] ToObjectArray() => [Case, ParamName];
}
