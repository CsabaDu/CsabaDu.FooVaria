namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.BehaviorObjects
{
    public sealed class LimiterObject : ILimiter
    {
        public LimitMode? LimitMode { private get; set; }

        public LimitMode? GetLimitMode() => LimitMode;
    }
}
