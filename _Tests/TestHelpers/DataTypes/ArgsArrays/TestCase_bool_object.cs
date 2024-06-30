namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_bool_object(string TestCase, bool IsTrue, object Obj) : TestCase_bool(TestCase, IsTrue)
{
    public override object[] ToObjectArray() => [TestCase, IsTrue, Obj];
}
