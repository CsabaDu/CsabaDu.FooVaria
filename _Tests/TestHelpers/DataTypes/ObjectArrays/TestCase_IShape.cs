namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_IShape(string Case, IShape Shape) : ObjectArray(Case)
    {
        public override object[] ToObjectArray() => [TestCase, Shape];
    }
}
