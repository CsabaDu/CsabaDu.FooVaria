namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_bool_Enum(string Case, bool IsTrue, Enum MeasureUnit) : bool_args(Case, IsTrue)
{
    public override object[] ToObjectArray() => [Case, IsTrue, MeasureUnit];
}
