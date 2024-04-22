namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_ILimiter(string TestCase, ILimiter Limiter) : ObjectArray(TestCase)
    {
        public override object[] ToObjectArray() => [TestCase, Limiter];
    }
}
