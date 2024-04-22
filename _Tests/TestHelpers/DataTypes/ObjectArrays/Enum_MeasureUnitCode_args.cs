namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Enum_MeasureUnitCode_args(string Case, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode) : Enum_args(Case, MeasureUnit)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, MeasureUnitCode];
}
