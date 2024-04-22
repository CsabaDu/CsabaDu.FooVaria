namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_bool_Enum_Enum(string Case, bool IsTrue, Enum MeasureUnit, Enum Context) : Args_bool_Enum(Case, IsTrue, MeasureUnit)
{
    public override object[] ToObjectArray() => [Case, IsTrue, MeasureUnit, Context];
}
