namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_IBaseMeasure_Enum(string TestCase, IBaseMeasure BaseMeasure, Enum MeasureUnit) : TestCase_IBaseMeasure(TestCase, BaseMeasure)
    {
        public override object[] ToObjectArray() => [TestCase, BaseMeasure, MeasureUnit];
    }
}
