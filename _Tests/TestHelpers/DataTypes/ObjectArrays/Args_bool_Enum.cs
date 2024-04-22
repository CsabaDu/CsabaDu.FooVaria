namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_bool_Enum(string Case, bool IsTrue, Enum MeasureUnit) : Args_bool(Case, IsTrue)
{
    public override object[] ToObjectArray() => [Case, IsTrue, MeasureUnit];
}
