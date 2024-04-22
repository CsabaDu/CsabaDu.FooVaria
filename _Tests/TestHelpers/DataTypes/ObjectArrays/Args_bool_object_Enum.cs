namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_bool_object_Enum(string Case, bool IsTrue, object Obj, Enum MeasureUnit) : Args_bool_object(Case, IsTrue, Obj)
{
    public override object[] ToObjectArray() => [Case, IsTrue, Obj, MeasureUnit];
}
