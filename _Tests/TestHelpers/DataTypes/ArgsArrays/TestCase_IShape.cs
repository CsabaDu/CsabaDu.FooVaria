namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_IShape(string Case, IShape Shape) : ArgsArray(Case)
    {
        public override object[] ToObjectArray() => [TestCase, Shape];
    }
}
