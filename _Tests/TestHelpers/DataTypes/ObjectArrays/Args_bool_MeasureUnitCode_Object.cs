namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_bool_MeasureUnitCode_Object(string Case, bool IsTrue, MeasureUnitCode MeasureUnitCode, object Obj) : Args_bool_MeasureUnitCode(Case, IsTrue, MeasureUnitCode)
{
    public override object[] ToObjectArray() => [Case, IsTrue];
}
