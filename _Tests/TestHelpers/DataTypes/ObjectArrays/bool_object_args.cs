namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;
public record bool_object_args(string Case, bool IsTrue, object Obj) : bool_args(Case, IsTrue)
{
    public override object[] ToObjectArray() => [Case, IsTrue, Obj];
}
