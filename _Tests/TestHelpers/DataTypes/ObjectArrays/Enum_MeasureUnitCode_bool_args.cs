namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Enum_MeasureUnitCode_bool_args(string Case, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, bool IsTrue) : Enum_MeasureUnitCode_args(Case, MeasureUnit, MeasureUnitCode)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, MeasureUnitCode, IsTrue];
}
