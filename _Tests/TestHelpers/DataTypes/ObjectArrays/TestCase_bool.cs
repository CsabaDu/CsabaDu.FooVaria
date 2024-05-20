namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_bool(string Case, bool IsTrue) : ObjectArray(Case)
    {
        public override object[] ToObjectArray() => [TestCase, IsTrue];
    }
}