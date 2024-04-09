namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;
public record bool_object_args(bool IsTrue, object Obj) : bool_arg(IsTrue)
{
    public override object[] ToObjectArray() => [IsTrue, Obj];
}
