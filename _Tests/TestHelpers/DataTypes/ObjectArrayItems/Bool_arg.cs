namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record Bool_arg(bool IsTrue) : ObjectArray
    {
        public override object[] ToObjectArray() => [IsTrue];
    }

    public record ILimiter_arg(ILimiter Limiter) : ObjectArray
    {
        public override object[] ToObjectArray() => [Limiter];
    }
}
