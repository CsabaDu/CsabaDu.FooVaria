namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_bool_object_Enum(string TestCase, bool IsTrue, object Obj, Enum MeasureUnit) : TestCase_bool_object(TestCase, IsTrue, Obj)
{
    public override object[] ToObjectArray() => [TestCase, IsTrue, Obj, MeasureUnit];
}
