namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record bool_MeasureUnitCode_object_args(bool IsTrue, MeasureUnitCode MeasureUnitCode, object Obj) : bool_MeasureUnitCode_args(IsTrue, MeasureUnitCode)
{
    public override object[] ToObjectArray() => [IsTrue];
}
