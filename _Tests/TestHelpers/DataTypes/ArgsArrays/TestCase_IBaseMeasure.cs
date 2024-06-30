namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_IBaseMeasure(string TestCase, IBaseMeasure BaseMeasure) : ArgsArray(TestCase)
    {
        public override object[] ToObjectArray() => [TestCase, BaseMeasure];
    }
}
