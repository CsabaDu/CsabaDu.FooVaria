namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_IShape_bool_IShape(string Case, IShape Shape, bool IsTrue, IShape Other) : TestCase_IShape_bool(Case, Shape, IsTrue)
    {
        public override object[] ToObjectArray() => [TestCase, Shape, IsTrue, Other];
    }
}
