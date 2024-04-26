namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_Enum_LimitMode_IShape(string TestCase, Enum MeasureUnit, LimitMode? LimitMode, IShape Shape) : TestCase_Enum_LimitMode(TestCase, MeasureUnit, LimitMode)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, LimitMode, Shape];
    }
}
