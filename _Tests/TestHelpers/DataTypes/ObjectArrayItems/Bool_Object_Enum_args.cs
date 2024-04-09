namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record bool_object_Enum_args(bool IsTrue, object Obj, Enum MeasureUnit) : bool_object_args(IsTrue, Obj)
{
    public override object[] ToObjectArray() => [IsTrue, Obj, MeasureUnit];
}
