namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_IShape_bool(string Case, IShape Shape, bool IsTrue) : TestCase_IShape(Case, Shape)
    {
        public override object[] ToObjectArray() => [TestCase, Shape, IsTrue];
    }
}
