namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_bool(string Case, bool IsTrue) : ArgsArray(Case)
    {
        public override object[] ToObjectArray() => [TestCase, IsTrue];
    }

    public record TestCase_IRootObject(string Case, IRootObject RootObject) : ArgsArray(Case)
    {
        public override object[] ToObjectArray() => [TestCase, RootObject];
    }
}