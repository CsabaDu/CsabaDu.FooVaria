namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseQuantifiables;

public sealed class LimiterObject : ILimiter
{
    public LimitMode? LimitMode { private get; set; }

    public LimitMode? GetLimitMode() => LimitMode;
}
