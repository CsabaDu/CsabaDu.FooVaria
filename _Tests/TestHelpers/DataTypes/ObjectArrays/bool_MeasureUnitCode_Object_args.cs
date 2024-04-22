namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record bool_MeasureUnitCode_object_args(string Case, bool IsTrue, MeasureUnitCode MeasureUnitCode, object Obj) : Args_bool_MeasureUnitCode(Case, IsTrue, MeasureUnitCode)
{
    public override object[] ToObjectArray() => [Case, IsTrue];
}
