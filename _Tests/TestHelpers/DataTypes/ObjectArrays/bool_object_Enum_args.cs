namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record bool_object_Enum_args(string Case, bool IsTrue, object Obj, Enum MeasureUnit) : bool_object_args(Case, IsTrue, Obj)
{
    public override object[] ToObjectArray() => [Case, IsTrue, Obj, MeasureUnit];
}
