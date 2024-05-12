namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_Enum_ILimiter_MeasureUnitCode(string TestCase, Enum MeasureUnit, ILimiter Limiter, MeasureUnitCode MeasureUnitCode) : TestCase_Enum_ILimiter(TestCase, MeasureUnit, Limiter)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, Limiter, MeasureUnitCode];
    }
}