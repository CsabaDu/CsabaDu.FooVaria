namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum(string Case, Enum MeasureUnit) : ObjectArray(Case)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit];
}
