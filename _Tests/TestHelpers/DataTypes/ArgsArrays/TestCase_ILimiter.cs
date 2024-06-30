namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_ILimiter(string TestCase, ILimiter Limiter) : ArgsArray(TestCase)
    {
        public override object[] ToObjectArray() => [TestCase, Limiter];
    }
}
