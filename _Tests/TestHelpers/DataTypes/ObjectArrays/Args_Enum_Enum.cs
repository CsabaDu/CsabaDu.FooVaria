namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_Enum(string Case, Enum MeasureUnit, Enum Context) : Args_Enum(Case, MeasureUnit)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, Context];
}
