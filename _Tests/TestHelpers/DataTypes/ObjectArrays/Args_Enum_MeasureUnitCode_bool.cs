namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_MeasureUnitCode_bool(string Case, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, bool IsTrue) : Args_Enum_MeasureUnitCode(Case, MeasureUnit, MeasureUnitCode)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, MeasureUnitCode, IsTrue];
}
