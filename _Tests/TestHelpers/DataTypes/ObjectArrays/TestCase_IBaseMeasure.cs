namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_IBaseMeasure(string TestCase, IBaseMeasure BaseMeasure) : ObjectArray(TestCase)
    {
        public override object[] ToObjectArray() => [TestCase, BaseMeasure];
    }
}
