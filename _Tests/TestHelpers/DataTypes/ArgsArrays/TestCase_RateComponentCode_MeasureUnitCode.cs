namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_RateComponentCode_MeasureUnitCode(string Case, RateComponentCode RateComponentCode, MeasureUnitCode MeasureUnitCode) : TestCase_RateComponentCode(Case, RateComponentCode)
    {
        public override object[] ToObjectArray() => [TestCase, RateComponentCode, MeasureUnitCode];
    }
}