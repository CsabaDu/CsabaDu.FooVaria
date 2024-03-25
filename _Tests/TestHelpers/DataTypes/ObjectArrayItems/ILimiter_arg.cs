namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record ILimiter_arg(ILimiter Limiter) : ObjectArray
    {
        public override object[] ToObjectArray() => [Limiter];
    }
}
