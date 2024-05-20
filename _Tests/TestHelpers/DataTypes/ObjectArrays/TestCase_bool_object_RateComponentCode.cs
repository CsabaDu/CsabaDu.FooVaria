namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_bool_object_RateComponentCode(string TestCase, bool IsTrue, object Obj, RateComponentCode RateComponentCode) : TestCase_bool_object(TestCase, IsTrue, Obj)
    {
        public override object[] ToObjectArray() => [TestCase, IsTrue, Obj, RateComponentCode];
    }
}