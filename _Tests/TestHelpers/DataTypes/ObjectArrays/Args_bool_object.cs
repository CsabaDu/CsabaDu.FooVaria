namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_bool_object(string Case, bool IsTrue, object Obj) : Args_bool(Case, IsTrue)
{
    public override object[] ToObjectArray() => [Case, IsTrue, Obj];
}
