namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record ILimiter_args(string Case, ILimiter Limiter) : ObjectArray(Case)
    {
        public override object[] ToObjectArray() => [Case, Limiter];
    }
}
