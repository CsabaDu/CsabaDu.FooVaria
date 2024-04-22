namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_MeasureUnitCode(string Case, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode) : Args_Enum(Case, MeasureUnit)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, MeasureUnitCode];
}
