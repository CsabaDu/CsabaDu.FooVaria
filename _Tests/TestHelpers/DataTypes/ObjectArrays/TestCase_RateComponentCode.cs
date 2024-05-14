namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_RateComponentCode(string Case, RateComponentCode RateComponentCode) : ObjectArray(Case)
    {
        public override object[] ToObjectArray() => [TestCase, RateComponentCode];
    }

    public record TestCase_RateComponentCode_MeasureUnitCode(string Case, RateComponentCode RateComponentCode, MeasureUnitCode MeasureUnitCode) : TestCase_RateComponentCode(Case, RateComponentCode)
    {
        public override object[] ToObjectArray() => [TestCase, RateComponentCode, MeasureUnitCode];
    }
}